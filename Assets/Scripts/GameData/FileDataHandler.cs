using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class FileDataHandler 
{
    private string dirPath = "";
    private string fileName = "";

    public FileDataHandler(string dirPath, string filePath)
    {
        this.dirPath = dirPath;
        this.fileName = filePath;
    }

    public void SaveData(Data data)
    {
        string fullPath = Path.Combine(dirPath, fileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            string dataToSave = JsonUtility.ToJson(data);

            using (FileStream fs = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.Write(dataToSave);
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
    public Data LoadData()
    {
        Data loadedData = null;

        string fullPath = Path.Combine(dirPath, fileName);

          if (File.Exists(fileName))
        {
            //try catch
            string datatoLoad = "";
            using (FileStream sr = new FileStream(fullPath, FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(sr))
                {
                      datatoLoad = reader.ReadToEnd();
                }
                
            }

            loadedData= JsonUtility.FromJson<Data>(datatoLoad);
        }

        return loadedData;
    }
}
