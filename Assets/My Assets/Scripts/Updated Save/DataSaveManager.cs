using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using System.Linq;
using UnityEngine.Rendering;
using System.IO;

public class DataSaveManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;
    [SerializeField] public bool removeSavefile;
    
    //File Extention for editing
    public string fileLocation;

    public static DataSaveManager instance { get; private set; }
    private List<IDataSave> dataSaveObjects;
    private FileDataHandler dataHandler;
    
    private GameData data;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Save Manager in the scene");
        }
        instance = this;
    }

    private void Start()
    {
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
        fileLocation = Application.persistentDataPath + "/" + fileName;
        dataSaveObjects = FindAlldataSaveObjects();
        LoadGame();
    }
    private void OnApplicationQuit()
    {
        SaveGame();
    }

    public void NewGame()
    {
        data = new GameData(); 
    }
    public void SaveGame()
    {
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
        dataSaveObjects = FindAlldataSaveObjects();
        foreach (IDataSave dataSaveObjects in dataSaveObjects)
        {
            dataSaveObjects.SaveData(ref data);
        }

        dataHandler.Save(data, removeSavefile);
        Debug.Log("DataSaveManager.cs 'Saved the game'");
    }
    public void LoadGame()
    {
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
        data = dataHandler.Load();


        if (data == null)
        {
            Debug.Log("No Savedata found");
            NewGame();
        }

        foreach (IDataSave dataSaveObjects in dataSaveObjects)
        {
            dataSaveObjects.LoadData(data);
        }
    }

    private List<IDataSave> FindAlldataSaveObjects()
    {
        IEnumerable<IDataSave> dataSaveObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataSave>();
        return new List<IDataSave>(dataSaveObjects);
    }
}
