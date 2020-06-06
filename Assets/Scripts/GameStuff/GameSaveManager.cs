using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameSaveManager : MonoBehaviour
{
    public static GameSaveManager gameSave;
    public List<ResetableScriptableObject> scriptables = new List<ResetableScriptableObject>();

    public void ResetScriptables()
    {
        //Delete Files
        for (int i = 0; i < scriptables.Count; i++)
        {
            if (File.Exists(FilePathFor(scriptables[i], i)))
            {
                File.Delete(FilePathFor(scriptables[i], i));
            }
        }
        //Reset Objects to default:
        scriptables.ForEach(obj => obj.Reset());
    }

    private void Awake()
    {
        if (gameSave == null)
        {
            gameSave = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this);
    }

    private void OnEnable()
    {
        LoadScriptables();
    }

    private void OnDisable()
    {
        SaveScriptables();
    }

    public void SaveScriptables()
    {
        for (int i = 0; i < scriptables.Count; i++)
        {
            FileStream file = File.Create(FilePathFor(scriptables[i], i));
            BinaryFormatter formatter = new BinaryFormatter();
            var json = JsonUtility.ToJson(scriptables[i]);
            formatter.Serialize(file, json);
            file.Close();
        }
    }

    public void LoadScriptables()
    {        
        for(int i = 0; i < scriptables.Count; i++)
        {
            if (File.Exists(FilePathFor(scriptables[i], i)))
            {
                FileStream file = File.Open(FilePathFor(scriptables[i], i), FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                JsonUtility.FromJsonOverwrite((string)formatter.Deserialize(file), scriptables[i]);
                file.Close();
            }
        }
    }

    private string FilePathFor(ScriptableObject obj, int count)
    {
        return Application.persistentDataPath
                + string.Format("/{0}.dat", count);
    }
}
