using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class XmlManager
{
    //public XmlManager xm = new XmlManager();
    public string GetDataPath()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
            return Application.persistentDataPath;
        else
            return Application.dataPath;
    }

    public string SerializeObject(object pObject, System.Type ty)
    {
        MemoryStream mStream = new MemoryStream();
        XmlSerializer xs = new XmlSerializer(ty);
        XmlTextWriter xmlWriter = new XmlTextWriter (mStream, Encoding.UTF8);
        xs.Serialize(xmlWriter, pObject);
        mStream = (MemoryStream)xmlWriter.BaseStream;
        return UTF8ByteArrayToString(mStream.ToArray());


    }
    public object DeserializeObject(string serializedString, System.Type ty)
    {
        XmlSerializer xs = new XmlSerializer(ty);
        MemoryStream mStream = new MemoryStream(StringToUTF8ByteArray(serializedString));
        return xs.Deserialize(mStream);
    }

    public string UTF8ByteArrayToString(byte[] bytes)
    {
        UTF8Encoding encoding = new UTF8Encoding();
        return encoding.GetString(bytes);
    }

    public byte[] StringToUTF8ByteArray(string dataString)
    {
        UTF8Encoding encoding = new UTF8Encoding();
        return encoding.GetBytes(dataString);
    }

    public void CreateXML(string filename, string dataString)
    {
        StreamWriter writer;
        writer = File.CreateText(filename);
        writer.Write(dataString);
        writer.Close();
    }


    public string LoadXML(string filename)
    {
        StreamReader reader = File.OpenText(filename);
        string dataString = reader.ReadToEnd();
        reader.Close();
        return dataString;
    }

    public bool HasFile(string filename)
    {
        return File.Exists(filename);
    }


    public void Save(string dataFileName, GameSaveData saveData)
    {
        string dataFilePath = GetDataPath() + "/" + dataFileName;
        string serializedDataString = SerializeObject(saveData, typeof(GameSaveData));
        CreateXML(dataFilePath, serializedDataString);
    }

    public void Load(string dataFileName, ref GameSaveData saveDataToLoad)
    {
        string dataFilePath = GetDataPath() + "/" + dataFileName;
        if(HasFile(dataFilePath))
        {
            string dataString = LoadXML(dataFilePath);
            GameSaveData saveData = DeserializeObject(dataString, typeof(GameSaveData)) as GameSaveData;
            if(saveData != null)
            {
                //return saveData;
                saveDataToLoad = saveData;
            }
            else
            {
                Debug.Log("Error! saveData is null!");
            }
        }
    }


    public void Awake()
    {
     

    }

    public void Start()
    {

        GameManager gm = GameManager.Instance;
    }
  
}
