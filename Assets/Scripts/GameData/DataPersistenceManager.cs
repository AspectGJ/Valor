using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("Database Config")]
    [SerializeField] private string dbName = "GameData";

    private SQLiteDataHandler sqliteDataHandler;
    public Data data;
    private Data data2;

    public static DataPersistenceManager instance { get; private set; }
    
    private void Awake()
    {
       if (instance == null)
    {
        instance = this;
        DontDestroyOnLoad(gameObject);

        sqliteDataHandler = GetComponent<SQLiteDataHandler>();
        if (sqliteDataHandler == null)
        {
            Debug.LogError("SQLiteDataHandler bileşeni bulunamadı veya atanamadı!");
        }

        // Yalnızca ilk örneği ayarladığımızda oyunu yükle
        LoadGame();
    }
    else
    {
        Destroy(gameObject);
    }
    }

    private void Start()
    {
        LoadGame();
    }

    public void NewGame()
    {
        data = new Data();
        data.playerAttributesData = new AttributesData(); // Yeni bir oyun başlatıldığında yeni bir AttributesData oluştur
        Debug.Log("Loaded player health: newgame" + data.playerAttributesData.healthPoint);
        SaveGame();
        Debug.Log("Loaded player health: newgame" + data.playerAttributesData.healthPoint);
    }

    public void LoadGame()
    {
        data = sqliteDataHandler.LoadData();

        if (data == null)
        {
            Debug.Log("yeni oyun baslatildi");
            NewGame();
        }

        //Debug.Log("Loaded player health: " + data.playerAttributesData.healthPoint);
    }

    public void SaveGame()
    {
        sqliteDataHandler.SaveData(data.playerAttributesData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}
