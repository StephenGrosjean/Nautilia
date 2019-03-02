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
    }
    //END SINGLETON//

    //Variables
    [SerializeField] private string nameFile = "database.xml";
    [SerializeField] private string path = "/XMLSaver/StreamingAssets/";

    //Database item
    public DataBase dataBase;


    //Save function
    public void Save() {
        //open xml file
        XmlSerializer serializer = new XmlSerializer(typeof(DataBase));

        //Create Stream
        FileStream stream = new FileStream(Application.dataPath + path + nameFile, FileMode.Create);
        //Serialize DB
        serializer.Serialize(stream, dataBase);

        //Close the stream
        stream.Close();
    }

    //Load function
    public void Load() {
        //open xml file
        XmlSerializer serializer = new XmlSerializer(typeof(DataBase));
        
        //Create Stream
        if (File.Exists(Application.dataPath + path + nameFile)) {
            FileStream stream = new FileStream(Application.dataPath + path + nameFile, FileMode.Open);

            //Deserialize DB
            dataBase = serializer.Deserialize(stream) as DataBase;

            //Close the stream
            stream.Close();
        }
        else {
            //Error message
            Debug.LogErrorFormat("<b><size=12><color=red>ERROR : File " + nameFile + " does not exist at path : </color><color=green> " + path + " </color> <color=red> (Maybe you did not save?) </color></size></b>");
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
    public int value;
}

//Data type info
[System.Serializable]
public class SecondDataset {
    public string name;
    public Vector2 pos;
}


