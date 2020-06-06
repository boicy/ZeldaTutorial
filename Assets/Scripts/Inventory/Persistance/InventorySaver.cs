using Leguar.TotalJSON;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;
using static SerialisableListString;

public class InventorySaver : MonoBehaviour
{
    [SerializeField] private PlayerInventory myInventory;

    public ItemDatabase itemDb;

    //SL is our serializable class that contains a representation of the items we want to save - this is a COPY
    private SerialisableListString SL = new SerialisableListString();


    private void OnEnable()
    {
        //clear the inventory
        myInventory.myInventory.Clear();
        Debug.Log("Inventory Count = " + myInventory.myInventory.Count);

        //clear the SL - we don't want anything in there
        SL.inventoryItemList.Clear();
        //Load our save file
        LoadScriptables();
        //re-import our save back into the game world
        ImportSaveData();
    }

    private void OnDisable()
    {
        //clear the SL 
        SL.inventoryItemList.Clear();
        // build our save data from our current game state
        BuildSaveData();
        //save out the save data
        SaveScriptables();
    }

    private void ImportSaveData()
    {
        Debug.Log("Import Save Data " + SL.inventoryItemList.Count);
        //go through the Sl and rebuild the items in the inventory
        for (int i = 0; i < SL.inventoryItemList.Count; i++)
        {

            //we will need the name and the count from the save data
            string name = SL.inventoryItemList[i].name;
            int count = SL.inventoryItemList[i].count;


            // we dont save the actual scriptable objects only a reference (NAME) that we then lookup to insert the correct scriptable object
            InventoryItem obj = itemDb.GetItem(name);
            if (obj)
            {
                // we have an object to restor - check how many of that item we need and set it 
                obj.numberHeld = count;

                // add the object to the inventory
                myInventory.myInventory.Add(obj);
                Debug.Log("Added " + obj.itemName + " count " + count + " to inventory");

            }
            else
            {
                //should never hit this!
                Debug.LogError("ITEM DB Not Found: " + SL.inventoryItemList[i].name);
            }
        }
    }

    private void BuildSaveData()
    {

        //go through the inventory and save out a key value pair of itemName and itemCount
        //then add to the serializablelist
        for (int i = 0; i < myInventory.myInventory.Count; i++)
        {
            //create a SerialItem and populate it from the inventory
            //SerializableListString.SerialItem SI = new SerializableListString.SerialItem();

            SerialItem SI = new SerialItem();
            SI.name = myInventory.myInventory[i].itemName;
            SI.count = myInventory.myInventory[i].numberHeld;

            //add to our SL - 
            SL.inventoryItemList.Add(SI);


        }
    }

    public void SaveScriptables()
    {
        //ResetScriptables();
        Debug.Log("IS: Saving to: " + Application.persistentDataPath);
        JSONSave();
    }


    private void JSONSave()
    {
        //filepath
        string filepath = Application.persistentDataPath + "/newsave.json";

        //create a streamwriter
        StreamWriter sw = new StreamWriter(filepath);

        //use the JSON library to serialize our serializableList into a JSON object
        JSON jsonObject = JSON.Serialize(SL);

        //turn that JSON object into a pretty formatted string
        string json = jsonObject.CreatePrettyString();

        //write to our file
        sw.WriteLine(json);

        //close the file
        sw.Close();
    }


    public void LoadScriptables()
    {
        Debug.Log("IS: Loading From: " + Application.persistentDataPath);
        JSONLoad();
    }


    public void JSONLoad()
    {

        //filepath
        string filepath = Application.persistentDataPath + "/newsave.json";

        if (File.Exists(filepath))
        {
            //read in the file to a string
            string json = File.ReadAllText(filepath);
            //use the JSON library to parse the string
            JSON jsonObject = JSON.ParseString(json);
            //deserialize the JSON object back into our Serializable class
            SL = jsonObject.Deserialize<SerialisableListString>();
        }

    }




}