using System.IO;
using UnityEngine;

public class FilePermissionsChecker : MonoBehaviour
{
    public string dbName = "GameData.db";
    private string dbFilePath;

    void Start()
    {
        dbFilePath = Path.Combine(Application.persistentDataPath, dbName);
        CheckFilePermissions();
    }

    void CheckFilePermissions()
    {
        if (File.Exists(dbFilePath))
        {
            Debug.Log("Database file exists at: " + dbFilePath);

            FileInfo fileInfo = new FileInfo(dbFilePath);
            Debug.Log("File Attributes: " + fileInfo.Attributes);

            // İzinleri kontrol etme
            if ((fileInfo.Attributes & FileAttributes.ReadOnly) != 0)
            {
                Debug.Log("File is read-only. Changing permissions...");
                fileInfo.Attributes &= ~FileAttributes.ReadOnly;
            }

            Debug.Log("File permissions set correctly.");
        }
        else
        {
            Debug.LogError("Database file not found at: " + dbFilePath);
        }
    }
}

