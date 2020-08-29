using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;

public class LoadSave : MonoBehaviour
{
     private string fileRoute;

    private static LoadSave instance;

    public static LoadSave Instance { get => instance;}

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }

        fileRoute = Application.persistentDataPath + "dataAnima.dat";

        Debug.Log(Application.persistentDataPath + "dataAnima.dat");

        LoadGame();
    }

    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(fileRoute);

        DataGame dataG = new DataGame();

        

        Debug.Log("Datos Guardados");

        bf.Serialize(file, dataG);

        file.Close();
    }

    public void LoadGame()
    {
        if(File.Exists(fileRoute))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(fileRoute, FileMode.Open);

            DataGame dataG = (DataGame)bf.Deserialize(file);

            Debug.Log("Datos cargados");

            file.Close();
        }
        else
        {
            Debug.Log("Datos originales cargados");
        }
    }

    [Serializable]
    class DataGame
    {
    }
}
