using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class InventorySaver : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory;

    private void OnEnable()
    {
        playerInventory.myInventory.Clear();
        LoadScriptables();
    }

    private void OnDisable()
    {
        SaveScriptables();
    }

    public void SaveScriptables()
    {
        ResetScriptables();
        var inv = playerInventory.myInventory;
        for (int i = 0; i < inv.Count; i++)
        {
            FileStream file = File.Create(FilePathFor(i));
            BinaryFormatter formatter = new BinaryFormatter();
            var json = JsonUtility.ToJson(inv[i]);
            formatter.Serialize(file, json);
            file.Close();
        }
    }

    public void LoadScriptables()
    {
        var inv = playerInventory.myInventory;
        int i = 0;

        while (File.Exists(FilePathFor(i)))
        {
            var temp = ScriptableObject.CreateInstance<InventoryItem>();
            FileStream file = File.Open(FilePathFor(i), FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            JsonUtility.FromJsonOverwrite((string)formatter.Deserialize(file), temp);
            file.Close();
            inv.Add(temp);
            i++;
        }

    }

    public void ResetScriptables()
    {
        var inv = playerInventory.myInventory;
        int i = 0;
        while (File.Exists(FilePathFor(i)))
        {
            File.Delete(FilePathFor(i));
            i++;
        }
    }

    private string FilePathFor(int count)
    {
        return Application.persistentDataPath
                + string.Format("/{0}.inv", count);
    }
}
