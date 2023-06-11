using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.Rendering;

public class FileDataHandler
{
    public string dataFilePath = "";

    public string dataFileName = "";

    private bool useEncryption = false;
    private readonly string EncryptionKey = "Key";

    public FileDataHandler(string dataFilePath, string dataFileName, bool useEncryption)
    {
        this.dataFilePath = dataFilePath;
        this.dataFileName = dataFileName;
        this.useEncryption = useEncryption;
    }

    public GameData Load()
    {
        // use Path.Combine to account for different OS's having different path separators
        string fullPath = Path.Combine(dataFilePath, dataFileName);
        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                // load the serialized data from the file
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                // optionally decrypt the data
                if (useEncryption)
                {
                    dataToLoad = EncryptDecrypt(dataToLoad);
                }

                // deserialize the data from Json back into the C# object
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Error occured when trying to load data from file: " + fullPath + "\n" + e);
            }
        }
        return loadedData;
    }

    public void Save(GameData data, bool removeSavefile)
    {
        string fullPath = Path.Combine(dataFilePath, dataFileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            //convert data to Json
            string dataToStore = JsonUtility.ToJson(data, true);

            if (useEncryption)
            {
                Debug.Log("Encrypting");
                dataToStore = EncryptDecrypt(dataToStore);
            }

            //write file
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }

        }
        catch (Exception e)
        {
            Debug.LogError("Error Occoured with saving gamedata to file: " + fullPath + "/n" + e);
        }

        if (removeSavefile)
        {
            Debug.Log("Removed Savedata");
            Directory.Delete(fullPath, true);
        }
    }

    // the below is a simple implementation of XOR encryption
    private string EncryptDecrypt(string data)
    {
        string modifiedData = "";
        for (int i = 0; i < data.Length; i++)
        {
            modifiedData += (char)(data[i] ^ EncryptionKey[i % EncryptionKey.Length]);
        }
        return modifiedData;
    }
}
