using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
/// <summary>
/// Made by Stephen Grosjean
/// V 0.1
/// https://github.com/StephenGrosjean/XMLSaver
/// </summary>

public class XMLSave : MonoBehaviour {

    //SINGLETON//
    public static XMLSave instance;

    private void Awake() {
        instance = this;
        CheckDataPath();
    }
    //END SINGLETON//

    //Variables
    [SerializeField] private string nameFile = "/database.xml";
    //[SerializeField] private string path = "/XMLSaver/StreamingAssets/";
    private string dataPath;

    //Database item
    public DataBase dataBase;

    //Save function
    public void Save() {
        //open xml file

        XmlSerializer serializer = new XmlSerializer(typeof(DataBase));

        //Create Stream
        
        FileStream stream = new FileStream(dataPath + nameFile, FileMode.Create);

        //Serialize DB
        serializer.Serialize(stream, dataBase);

        //Close the stream
        stream.Close();
    }

    //Load function
    public void Load() {
        CheckDataPath();

        //open xml file
        XmlSerializer serializer = new XmlSerializer(typeof(DataBase));
        
        //Create Stream
        if (File.Exists(dataPath + nameFile)) {
            FileStream stream = new FileStream(dataPath + nameFile, FileMode.Open);

            //Deserialize DB
            dataBase = serializer.Deserialize(stream) as DataBase;

            //Close the stream
            stream.Close();
        }
        else {
            //Error message
            Debug.LogErrorFormat("<b><size=12><color=red>ERROR : File " + nameFile + " does not exist at path : </color><color=green> " + dataPath + nameFile + " </color> <color=red> (Maybe you did not save?) </color></size></b>");
        }
    }

    void CheckDataPath() {
        if (Application.platform == RuntimePlatform.Android) {
            dataPath = Application.persistentDataPath;
        }
        else {
            dataPath = Application.streamingAssetsPath;
        }
    }

}

//What to save 
[System.Serializable]
public class DataBase {

    [XmlArray("First_Dataset")] //Name in XML file (avoid spaces)
    public List<FirstDataset> firstDB = new List<FirstDataset>(); //Class to save

    [XmlArray("Second_Dataset")] 
    public List<SecondDataset> secondDB = new List<SecondDataset>(); 
}


//Data type info
[System.Serializable]
public class FirstDataset {
    public string name;
    public float value;
}

//Data type info
[System.Serializable]
public class SecondDataset {
    public string name;
    public Vector2 pos;
}


