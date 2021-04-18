using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

public static class SaveAndLoad
{
    private static string filePath = Application.dataPath + "/Saves/";

    public static void SaveToPrefs(string keyName, object file)
    {
        string saveData = JsonUtility.ToJson(file);
        
        PlayerPrefs.SetString(keyName, saveData);
        
        PlayerPrefs.Save();
        
        Debug.Log("Object saved to prefs by key " + keyName);
    }

    public static T LoadFromPrefs<T>(string keyName)
    {
        if (PlayerPrefs.HasKey(keyName))
        {
            string loadData = PlayerPrefs.GetString(keyName);
            
            T deserializedObject = JsonUtility.FromJson<T>(loadData);
            
            Debug.Log("Object loaded from prefs by key " +  keyName);

            return deserializedObject;
        }
        else
        {
            Debug.LogError("Don't have key " + keyName + " in prefs");
            return default(T);
        }
    }
    
    public static void SaveToTxt(string fileName, object file)
    {
        if (!Directory.Exists(filePath))
        {
            Directory.CreateDirectory(filePath);
        }
        
        try
        {
            using (FileStream fs = new FileStream(filePath + fileName, FileMode.OpenOrCreate))
            {
                string saveData = JsonUtility.ToJson(file);

                byte[] rawData = Encoding.UTF8.GetBytes(saveData);
                
                fs.Write(rawData, 0, rawData.Length);
 
                Debug.Log("Object saved to " + filePath + fileName);
            }
        }
        catch (IOException e)
        {
            Debug.LogError("Fail to save file. " + e.Message);
        }
    }
    
    public static T LoadFromTxt<T>(string fileName)
    {
        if (File.Exists(filePath + fileName))
        {
            try
            {
                using (FileStream fs = new FileStream(filePath + fileName, FileMode.Open))
                {
                    byte[] rawData = new byte[fs.Length];

                    fs.Read(rawData, 0, rawData.Length);

                    string loadData = Encoding.UTF8.GetString(rawData);

                    T deserializedObject = JsonUtility.FromJson<T>(loadData);
 
                    Debug.Log("Object loaded from " + filePath + fileName);

                    return deserializedObject;
                }
            }
            catch (IOException e)
            {
                Debug.LogError("Fail to load file. " + e.Message);
                return default(T);
            }
        }
        else
        {
            Debug.LogError("File " + fileName + " doesn't exist by Path " + filePath);
            return default(T);
        }
    }
    
    public static void SaveToBinary(string fileName, object file)
    {
        if (!Directory.Exists(filePath))
        {
            Directory.CreateDirectory(filePath);
        }
        
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(filePath + fileName, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, file);
 
                Debug.Log("Object saved to " + filePath + fileName);
            }
        }
        catch (IOException e)
        {
            Debug.LogError("Fail to save file. " + e.Message);
        }
    }
    
    public static object LoadFromBinary(string fileName)
    {
        if (File.Exists(filePath + fileName))
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fs = new FileStream(filePath + fileName, FileMode.Open))
                {
                    object deserializedObject = formatter.Deserialize(fs);
 
                    Debug.Log("Object loaded from " + filePath + fileName);

                    return deserializedObject;
                }
            }
            catch (IOException e)
            {
                Debug.LogError("Fail to load file. " + e.Message);
                return null;
            }
        }
        else
        {
            Debug.LogError("File " + fileName + " doesn't exist by Path " + filePath);
            return null;
        }
    }
}
