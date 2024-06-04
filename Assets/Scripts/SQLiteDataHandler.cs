using System.Collections;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using UnityEngine.Networking;
using System;

public class SQLiteDataHandler : MonoBehaviour
{
    public string dbName = "GameData";
    private string connectionString;
    public bool Initialized { get; private set; } = false;

    private void Awake()
    {
        StartCoroutine(CopyDatabase());
    }

    private IEnumerator CopyDatabase()
    {
        string dbFileName = $"{dbName}.db";
        string dbFilePath = Path.Combine(Application.persistentDataPath, dbFileName);

        if (!File.Exists(dbFilePath))
        {
            string sourcePath = Path.Combine(Application.streamingAssetsPath, dbFileName);

            if (Application.platform == RuntimePlatform.Android)
            {
                UnityWebRequest loadDb = UnityWebRequest.Get(sourcePath);
                yield return loadDb.SendWebRequest();

                if (loadDb.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError("Failed to load database from StreamingAssets: " + loadDb.error);
                    yield break;
                }

                File.WriteAllBytes(dbFilePath, loadDb.downloadHandler.data);
            }
            else
            {
                File.Copy(sourcePath, dbFilePath);
            }

            Debug.Log("Database copied to persistentDataPath");
        }
        else
        {
            Debug.Log("Database already exists at persistentDataPath");
        }

        InitializeConnectionString(dbFilePath);

        CreateDatabase();
        CreateTable();
        CreateTableShaman();

        Initialized = true;
    }

    private void InitializeConnectionString(string dbFilePath)
    {
        connectionString = $"URI=file:{dbFilePath}";
        Debug.Log("SQLite Database Path: " + dbFilePath);
    }

    private void CreateDatabase()
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            connection.Close();
            Debug.Log("Main database created.");
        }
    }

    private void CreateTable()
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = "CREATE TABLE IF NOT EXISTS PlayerData (id INTEGER PRIMARY KEY, healthPoint INTEGER, mana INTEGER, attackmin INTEGER, attackmax INTEGER, attackCost INTEGER, StrongAttackmin INTEGER, StrongAttackmax INTEGER, StrongAttackCost INTEGER, DeepSharpnessmin INTEGER, DeepSharpnessmax INTEGER, DeepSharpnessCost INTEGER)";
                dbCmd.CommandText = sqlQuery;
                dbCmd.ExecuteNonQuery();
            }

            dbConnection.Close();
        }
        ShowTables();
    }

    private void CreateTableShaman()
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = "CREATE TABLE IF NOT EXISTS ShamanData (id INTEGER PRIMARY KEY, healthPoint INTEGER, mana INTEGER, attackmin INTEGER, attackmax INTEGER, attackCost INTEGER, StrongAttackmin INTEGER, StrongAttackmax INTEGER, StrongAttackCost INTEGER, DeepSharpnessmin INTEGER, DeepSharpnessmax INTEGER, DeepSharpnessCost INTEGER)";
                dbCmd.CommandText = sqlQuery;
                dbCmd.ExecuteNonQuery();
            }

            dbConnection.Close();
        }
        ShowTables();
    }

    public void SaveData(AttributesData data)
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string checkQuery = "SELECT COUNT(*) FROM PlayerData WHERE id = 1";
                dbCmd.CommandText = checkQuery;
                int count = Convert.ToInt32(dbCmd.ExecuteScalar());

                if (count == 0)
                {
                    string insertQuery = "INSERT INTO PlayerData (id, healthPoint, mana, attackmin, attackmax, attackCost, StrongAttackmin, StrongAttackmax, StrongAttackCost, DeepSharpnessmin, DeepSharpnessmax, DeepSharpnessCost) " +
                    "VALUES (1, @HealthPoint, @Mana, @AttackMin, @AttackMax, @AttackCost, @StrongAttackMin, @StrongAttackMax, @StrongAttackCost, @DeepSharpnessMin, @DeepSharpnessMax, @DeepSharpnessCost)";
                    dbCmd.CommandText = insertQuery;
                    Debug.Log("Inserting new data into PlayerData");
                }
                else
                {
                    string updateQuery = "UPDATE PlayerData SET healthPoint = @HealthPoint, mana = @Mana, attackmin = @AttackMin, attackmax = @AttackMax, attackCost = @AttackCost, " +
                    "StrongAttackmin = @StrongAttackMin, StrongAttackmax = @StrongAttackMax, StrongAttackCost = @StrongAttackCost, " +
                    "DeepSharpnessmin = @DeepSharpnessMin, DeepSharpnessmax = @DeepSharpnessMax, DeepSharpnessCost = @DeepSharpnessCost WHERE id = 1";
                    dbCmd.CommandText = updateQuery;
                    Debug.Log("Updating existing data in PlayerData");
                }

                dbCmd.Parameters.Add(new SqliteParameter("@HealthPoint", data.healthPoint));
                dbCmd.Parameters.Add(new SqliteParameter("@Mana", data.mana));
                dbCmd.Parameters.Add(new SqliteParameter("@AttackMin", data.attackmin));
                dbCmd.Parameters.Add(new SqliteParameter("@AttackMax", data.attackmax));
                dbCmd.Parameters.Add(new SqliteParameter("@AttackCost", data.attackCost));
                dbCmd.Parameters.Add(new SqliteParameter("@StrongAttackMin", data.StrongAttackmin));
                dbCmd.Parameters.Add(new SqliteParameter("@StrongAttackMax", data.StrongAttackmax));
                dbCmd.Parameters.Add(new SqliteParameter("@StrongAttackCost", data.StrongAttackCost));
                dbCmd.Parameters.Add(new SqliteParameter("@DeepSharpnessMin", data.DeepSharpnessmin));
                dbCmd.Parameters.Add(new SqliteParameter("@DeepSharpnessMax", data.DeepSharpnessmax));
                dbCmd.Parameters.Add(new SqliteParameter("@DeepSharpnessCost", data.DeepSharpnessCost));

                dbCmd.ExecuteNonQuery();
            }

            dbConnection.Close();
        }
    }

    public void SaveDataShaman(AttributesData data)
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string checkQuery = "SELECT COUNT(*) FROM ShamanData WHERE id = 1";
                dbCmd.CommandText = checkQuery;
                int count = Convert.ToInt32(dbCmd.ExecuteScalar());

                if (count == 0)
                {
                    string insertQuery = "INSERT INTO ShamanData (id, healthPoint, mana, attackmin, attackmax, attackCost, StrongAttackmin, StrongAttackmax, StrongAttackCost, DeepSharpnessmin, DeepSharpnessmax, DeepSharpnessCost) " +
                    "VALUES (1, @HealthPoint, @Mana, @AttackMin, @AttackMax, @AttackCost, @StrongAttackMin, @StrongAttackMax, @StrongAttackCost, @DeepSharpnessMin, @DeepSharpnessMax, @DeepSharpnessCost)";
                    dbCmd.CommandText = insertQuery;
                    Debug.Log("Inserting new data into ShamanData");
                }
                else
                {
                    string updateQuery = "UPDATE ShamanData SET healthPoint = @HealthPoint, mana = @Mana, attackmin = @AttackMin, attackmax = @AttackMax, attackCost = @AttackCost, " +
                    "StrongAttackmin = @StrongAttackMin, StrongAttackmax = @StrongAttackMax, StrongAttackCost = @StrongAttackCost, " +
                    "DeepSharpnessmin = @DeepSharpnessMin, DeepSharpnessmax = @DeepSharpnessMax, DeepSharpnessCost = @DeepSharpnessCost WHERE id = 1";
                    dbCmd.CommandText = updateQuery;
                    Debug.Log("Updating existing data in ShamanData");
                }

                dbCmd.Parameters.Add(new SqliteParameter("@HealthPoint", data.healthPoint));
                dbCmd.Parameters.Add(new SqliteParameter("@Mana", data.mana));
                dbCmd.Parameters.Add(new SqliteParameter("@AttackMin", data.attackmin));
                dbCmd.Parameters.Add(new SqliteParameter("@AttackMax", data.attackmax));
                dbCmd.Parameters.Add(new SqliteParameter("@AttackCost", data.attackCost));
                dbCmd.Parameters.Add(new SqliteParameter("@StrongAttackMin", data.StrongAttackmin));
                dbCmd.Parameters.Add(new SqliteParameter("@StrongAttackMax", data.StrongAttackmax));
                dbCmd.Parameters.Add(new SqliteParameter("@StrongAttackCost", data.StrongAttackCost));
                dbCmd.Parameters.Add(new SqliteParameter("@DeepSharpnessMin", data.DeepSharpnessmin));
                dbCmd.Parameters.Add(new SqliteParameter("@DeepSharpnessMax", data.DeepSharpnessmax));
                dbCmd.Parameters.Add(new SqliteParameter("@DeepSharpnessCost", data.DeepSharpnessCost));

                dbCmd.ExecuteNonQuery();
            }

            dbConnection.Close();
        }
    }

    public Data LoadData(Data loadedData)
    {
        if (loadedData == null)
        {
            Debug.LogError("Loaded data is null");
            return null;
        }

        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = "SELECT * FROM PlayerData WHERE id = 1";
                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        loadedData.playerAttributesData.healthPoint = reader.GetInt32(1);
                        loadedData.playerAttributesData.mana = reader.GetInt32(2);
                        loadedData.playerAttributesData.attackmin = reader.GetInt32(3);
                        loadedData.playerAttributesData.attackmax = reader.GetInt32(4);
                        loadedData.playerAttributesData.attackCost = reader.GetInt32(5);
                        loadedData.playerAttributesData.StrongAttackmin = reader.GetInt32(6);
                        loadedData.playerAttributesData.StrongAttackmax = reader.GetInt32(7);
                        loadedData.playerAttributesData.StrongAttackCost = reader.GetInt32(8);
                        loadedData.playerAttributesData.DeepSharpnessmin = reader.GetInt32(9);
                        loadedData.playerAttributesData.DeepSharpnessmax = reader.GetInt32(10);
                        loadedData.playerAttributesData.DeepSharpnessCost = reader.GetInt32(11);

                        Debug.Log("Data loaded successfully");
                        Debug.Log("playerhp: " + loadedData.playerAttributesData.healthPoint);
                    }
                    else
                    {
                        Debug.LogError("No data found in the database");
                    }
                }
            }

            dbConnection.Close();
        }

        return loadedData;
    }

    public Data LoadDataShaman(Data loadedData)
    {
        if (loadedData == null)
        {
            Debug.LogError("Loaded data is null");
            return null;
        }

        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = "SELECT * FROM ShamanData WHERE id = 1";
                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        loadedData.shamanAttributesData.healthPoint = reader.GetInt32(1);
                        loadedData.shamanAttributesData.mana = reader.GetInt32(2);
                        loadedData.shamanAttributesData.attackmin = reader.GetInt32(3);
                        loadedData.shamanAttributesData.attackmax = reader.GetInt32(4);
                        loadedData.shamanAttributesData.attackCost = reader.GetInt32(5);
                        loadedData.shamanAttributesData.StrongAttackmin = reader.GetInt32(6);
                        loadedData.shamanAttributesData.StrongAttackmax = reader.GetInt32(7);
                        loadedData.shamanAttributesData.StrongAttackCost = reader.GetInt32(8);
                        loadedData.shamanAttributesData.DeepSharpnessmin = reader.GetInt32(9);
                        loadedData.shamanAttributesData.DeepSharpnessmax = reader.GetInt32(10);
                        loadedData.shamanAttributesData.DeepSharpnessCost = reader.GetInt32(11);

                        Debug.Log("Shaman Data loaded successfully");
                        Debug.Log("shamanhp: " + loadedData.shamanAttributesData.healthPoint);
                    }
                    else
                    {
                        Debug.LogError("No data found in the database");
                    }
                }
            }

            dbConnection.Close();
        }

        return loadedData;
    }

    private void ShowTables()
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                dbCmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table'";
                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string tableName = reader.GetString(0);
                        Debug.Log("Table Name: " + tableName);
                    }
                }
            }

            dbConnection.Close();
        }
    }
}
