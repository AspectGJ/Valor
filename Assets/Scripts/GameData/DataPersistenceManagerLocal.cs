using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataPersistenceManagerLocal : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    public Data data = new Data();

    private FileDataHandler fileDataHandler;
    public List<IPersistence> persistenceObjects;

    public static DataPersistenceManagerLocal instance { get; private set; }

    private void Awake() {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            //print("Path: " + Application.persistentDataPath + " File Name: " + fileName);
            fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start() {
        //this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        persistenceObjects = FindAllPersistenceObjects();
        if(persistenceObjects == null || persistenceObjects.Count == 0)
        {
            //Debug.LogError("No IPersistence objects found in the scene!");
        }
        LoadGame();
    }

    public void NewGame()
    {
        data = new Data();
        //Debug.Log("New game initilialized with default values!" + this.data.ToString());
    }

    public void LoadGame()
    {   
        data = fileDataHandler.LoadData();
      
        if(data == null)
        {
            NewGame();
        }

        else {
            //print("Loaded data: " + data.ToString());
        }

        if (persistenceObjects == null)
        {
            //Debug.LogError("Persistence objects list is null in LoadGame!");
            return;
        }

        foreach (IPersistence persistenceObject in persistenceObjects)
        {
            if (persistenceObject == null)
            {
                //Debug.LogError("Persistence object is null! deep");
                continue;
            }
            persistenceObject.LoadData(data);
        }

        

        //print("Loaded player health: " + data.playerAttributesData.healthPoint);
    }

    public void SaveGame()
    {
        foreach (IPersistence persistenceObject in persistenceObjects)
        {
            //print("Saving data for: " + persistenceObject.ToString());
            persistenceObject.SaveData(data);
        }

        //print("Saved player health: " + data.playerAttributesData.healthPoint);
        //print("Saved shaman health: " + data.shamanAttributesData.healthPoint);

        fileDataHandler.SaveData(data);

    }

    private void OnApplicationQuit() {
        SaveGame();
    }

    private List<IPersistence> FindAllPersistenceObjects()
    {
        IEnumerable<IPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>(true).OfType<IPersistence>();
        //Debug.Log("Found persistence objects count: " + dataPersistenceObjects.Count());
        return dataPersistenceObjects.ToList();
    }
}
