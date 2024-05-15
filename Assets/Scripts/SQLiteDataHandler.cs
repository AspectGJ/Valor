using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;

public class SQLiteDataHandler : MonoBehaviour
{
    public string dbName = "GameData";
    private string connectionString;

    private void Start()
    {
        connectionString = $"URI=file:{Application.persistentDataPath}/{dbName}.db";
        // CreateDatabase();
         CreateTable();
        // ShowDatabasePath();
        // ShowTables();
    }

    private void ShowDatabasePath()
    {
        string databasePath = $"{Application.persistentDataPath}/{dbName}.db";
        Debug.Log("SQLite Database Path: " + databasePath);
    }

    private void CreateDatabase()
    {
        using (var connection = new SqliteConnection($"URI=file:{Application.persistentDataPath}/master.db"))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = $"ATTACH DATABASE '{Application.persistentDataPath}/{dbName}.db' AS {dbName}";
            command.ExecuteNonQuery();
            connection.Close();
        }
        Debug.Log("Database oluşturuldu.");
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

    public void SaveData(AttributesData data)
    {

        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                // update edilecek
                // string sqlQuery = "INSERT INTO PlayerData (healthPoint, mana, attackmin, attackmax, attackCost, StrongAttackmin, StrongAttackmax, StrongAttackCost, DeepSharpnessmin, DeepSharpnessmax, DeepSharpnessCost) " +
                //                   "VALUES (@HealthPoint, @Mana, @AttackMin, @AttackMax, @AttackCost, @StrongAttackMin, @StrongAttackMax, @StrongAttackCost, @DeepSharpnessMin, @DeepSharpnessMax, @DeepSharpnessCost)";
                string sqlQuery = "UPDATE PlayerData  SET healthPoint = @HealthPoint, mana = @Mana, attackmin = @AttackMin, attackmax = @AttackMax, attackCost = @AttackCost, StrongAttackmin = @StrongAttackMin, StrongAttackmax = @StrongAttackMax, StrongAttackCost = @StrongAttackCost, DeepSharpnessmin = @DeepSharpnessMin, DeepSharpnessmax = @DeepSharpnessMax, DeepSharpnessCost = @DeepSharpnessCost  WHERE id = 1 "; 

                dbCmd.CommandText = sqlQuery;
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

    public Data LoadData()
{
    Data data = new Data();
    data.playerAttributesData = new AttributesData();

    using (IDbConnection dbConnection = new SqliteConnection(connectionString))
    {
        dbConnection.Open();

        using (IDbCommand dbCmd = dbConnection.CreateCommand())
        {
            string sqlQuery = "SELECT * FROM PlayerData";
            dbCmd.CommandText = sqlQuery;

            using (IDataReader reader = dbCmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Debug.Log("veriler okunuyor");
                    data.playerAttributesData.healthPoint = reader.GetInt32(1);
                    data.playerAttributesData.mana = reader.GetInt32(2);
                    data.playerAttributesData.attackmin = reader.GetInt32(3);
                    data.playerAttributesData.attackmax = reader.GetInt32(4);
                    data.playerAttributesData.attackCost = reader.GetInt32(5);
                    data.playerAttributesData.StrongAttackmin = reader.GetInt32(6);
                    data.playerAttributesData.StrongAttackmax = reader.GetInt32(7);
                    data.playerAttributesData.StrongAttackCost = reader.GetInt32(8);
                    data.playerAttributesData.DeepSharpnessmin = reader.GetInt32(9);
                    data.playerAttributesData.DeepSharpnessmax = reader.GetInt32(10);
                    data.playerAttributesData.DeepSharpnessCost = reader.GetInt32(11);
                    Debug.Log("playerhp: " + data.playerAttributesData.healthPoint);
                }
                    Debug.Log("veriler okundu");

            }
        }

        dbConnection.Close();
    }

    return data;
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

