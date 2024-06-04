using System.Collections;
using UnityEngine;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("Database Config")]
    [SerializeField] private string dbName = "GameData";
    public AttributesScriptableObject playerAttributesOS;
    public AttributesScriptableObject shamanAttributesOS;
    public SQLiteDataHandler sqliteDataHandler;

    public Data data;
    public Data data2;

    public static DataPersistenceManager instance { get; private set; }

    private IEnumerator Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            if (sqliteDataHandler == null)
            {
                Debug.LogError("SQLiteDataHandler component not found or not assigned!");
                yield break;
            }

            yield return new WaitUntil(() => sqliteDataHandler.Initialized);

            LoadGame();
        }
        else
        {
            Destroy(gameObject);
            yield break;
        }
    }

    public void NewGame()
    {
        data2 = new Data();
        data = new Data();
        data.playerAttributesData = new AttributesData(true);
        data2.shamanAttributesData = new AttributesData(false);

        playerAttributesOS.healthPoint = data.playerAttributesData.healthPoint;
        playerAttributesOS.mana = data.playerAttributesData.mana;
        playerAttributesOS.attackmin = data.playerAttributesData.attackmin;
        playerAttributesOS.attackmax = data.playerAttributesData.attackmax;
        playerAttributesOS.attackCost = data.playerAttributesData.attackCost;
        playerAttributesOS.StrongAttackmin = data.playerAttributesData.StrongAttackmin;
        playerAttributesOS.StrongAttackmax = data.playerAttributesData.StrongAttackmax;
        playerAttributesOS.StrongAttackCost = data.playerAttributesData.StrongAttackCost;
        playerAttributesOS.DeepSharpnessmin = data.playerAttributesData.DeepSharpnessmin;
        playerAttributesOS.DeepSharpnessmax = data.playerAttributesData.DeepSharpnessmax;
        playerAttributesOS.DeepSharpnessCost = data.playerAttributesData.DeepSharpnessCost;

        shamanAttributesOS.healthPoint = data2.shamanAttributesData.healthPoint;
        shamanAttributesOS.mana = data2.shamanAttributesData.mana;
        shamanAttributesOS.attackmin = data2.shamanAttributesData.attackmin;
        shamanAttributesOS.attackmax = data2.shamanAttributesData.attackmax;
        shamanAttributesOS.attackCost = data2.shamanAttributesData.attackCost;
        shamanAttributesOS.StrongAttackmin = data2.shamanAttributesData.StrongAttackmin;
        shamanAttributesOS.StrongAttackmax = data2.shamanAttributesData.StrongAttackmax;
        shamanAttributesOS.StrongAttackCost = data2.shamanAttributesData.StrongAttackCost;
        shamanAttributesOS.DeepSharpnessmin = data2.shamanAttributesData.DeepSharpnessmin;
        shamanAttributesOS.DeepSharpnessmax = data2.shamanAttributesData.DeepSharpnessmax;
        shamanAttributesOS.DeepSharpnessCost = data2.shamanAttributesData.DeepSharpnessCost;

        Debug.Log("New game player health: " + data.playerAttributesData.healthPoint);
        SaveGame();
    }

    public void LoadGame()
    {
        data = sqliteDataHandler.LoadData(new Data());
        data2 = sqliteDataHandler.LoadDataShaman(new Data());

        if (data == null)
        {
            Debug.Log("Starting new game...");
            NewGame();
        }
        else
        {
            playerAttributesOS.healthPoint = data.playerAttributesData.healthPoint;
            playerAttributesOS.mana = data.playerAttributesData.mana;
            playerAttributesOS.attackmin = data.playerAttributesData.attackmin;
            playerAttributesOS.attackmax = data.playerAttributesData.attackmax;
            playerAttributesOS.attackCost = data.playerAttributesData.attackCost;
            playerAttributesOS.StrongAttackmin = data.playerAttributesData.StrongAttackmin;
            playerAttributesOS.StrongAttackmax = data.playerAttributesData.StrongAttackmax;
            playerAttributesOS.StrongAttackCost = data.playerAttributesData.StrongAttackCost;
            playerAttributesOS.DeepSharpnessmin = data.playerAttributesData.DeepSharpnessmin;
            playerAttributesOS.DeepSharpnessmax = data.playerAttributesData.DeepSharpnessmax;
            playerAttributesOS.DeepSharpnessCost = data.playerAttributesData.DeepSharpnessCost;

            shamanAttributesOS.healthPoint = data2.shamanAttributesData.healthPoint;
            shamanAttributesOS.mana = data2.shamanAttributesData.mana;
            shamanAttributesOS.attackmin = data2.shamanAttributesData.attackmin;
            shamanAttributesOS.attackmax = data2.shamanAttributesData.attackmax;
            shamanAttributesOS.attackCost = data2.shamanAttributesData.attackCost;
            shamanAttributesOS.StrongAttackmin = data2.shamanAttributesData.StrongAttackmin;
            shamanAttributesOS.StrongAttackmax = data2.shamanAttributesData.StrongAttackmax;
            shamanAttributesOS.StrongAttackCost = data2.shamanAttributesData.StrongAttackCost;
            shamanAttributesOS.DeepSharpnessmin = data2.shamanAttributesData.DeepSharpnessmin;
            shamanAttributesOS.DeepSharpnessmax = data2.shamanAttributesData.DeepSharpnessmax;
            shamanAttributesOS.DeepSharpnessCost = data2.shamanAttributesData.DeepSharpnessCost;

            Debug.Log("Loaded player health: " + playerAttributesOS.healthPoint);
            Debug.Log("Loaded shaman health: " + shamanAttributesOS.healthPoint);
        }
    }

    public void SaveGame()
    {
        sqliteDataHandler.SaveData(data.playerAttributesData);
        sqliteDataHandler.SaveDataShaman(data2.shamanAttributesData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}
