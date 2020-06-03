using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameSaveManager : MonoBehaviour
{
    public static GameSaveManager gameSave;
    public List<ScriptableObject> objects = new List<ScriptableObject>();

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
        objects.ForEach(Save);
    }

    private void Save(ScriptableObject obj)
    {
        FileStream file = File.Create(FilePath(obj));
        BinaryFormatter formatter = new BinaryFormatter();
        var json = JsonUtility.ToJson(obj);
        formatter.Serialize(file, json);
        file.Close();
    }

    public void LoadScriptables()
    {
        objects.ForEach(Load);
    }

    private void Load(ScriptableObject obj)
    {
        if (File.Exists(FilePath(obj))) {
            FileStream file = File.Open(FilePath(obj), FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            JsonUtility.FromJsonOverwrite((string)formatter.Deserialize(file), obj);
            file.Close();
        }
    }

    private static string FilePath(ScriptableObject obj)
    {
        return Application.persistentDataPath
                + string.Format("/{0}.dat", obj.GetHashCode());
    }
}
