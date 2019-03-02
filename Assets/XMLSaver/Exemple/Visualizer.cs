using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Visualizer for the XMLSave (Only for the test Dataset)
/// </summary>
public class Visualizer : MonoBehaviour {

    [SerializeField] private GameObject firstDataPrefab; //First data visualizer
    [SerializeField] private GameObject secondDataPrefab; //Second data visualizer
    [SerializeField] private XMLSave xmlSaveScript; //The XMLSave script

    [SerializeField] private Transform dataContainer; //Where the data blocs will be displayed

    private List<GameObject> dataVisu = new List<GameObject>(); //List of all blocs

    //Load the XML (Button usage)
    public void LoadXML() {

        //Destroy all blocs if there is any
        if (dataVisu.Count != 0) {
            foreach (GameObject datavisu in dataVisu) {
                Destroy(datavisu);
            }
        }
        //Clear the list
        dataVisu.Clear();

        //Load the XML
        xmlSaveScript.Load();

        //Display the blocs
        Display();
    }


    //Instantiate blocs 
    void Display() {

        //Find all dataset in the firstDB 
        foreach(FirstDataset dataset in xmlSaveScript.dataBase.firstDB) {
            //Instantiate a bloc
            GameObject visu = Instantiate(firstDataPrefab, transform.position, transform.rotation); 

            //Set the parent of the bloc
            visu.transform.parent = dataContainer;

            //Set the color to green
            visu.GetComponent<Image>().color = Color.green; 

            //Set the name to dataset.name
            visu.transform.Find("Name").GetComponent<Text>().text = dataset.name;

            //Set the value to dataset.value (convert to string)
            visu.transform.Find("Value").GetComponent<Text>().text = dataset.value.ToString();

            //Add the bloc to the list
            dataVisu.Add(visu);

        }

        //Same thing ad the first dataset with the second dataset
        foreach (SecondDataset dataset in xmlSaveScript.dataBase.secondDB) {
            GameObject visu = Instantiate(secondDataPrefab, transform.position, transform.rotation);
            visu.transform.parent = dataContainer;
            visu.GetComponent<Image>().color = Color.red;

            visu.transform.Find("Name").GetComponent<Text>().text = dataset.name;
            visu.transform.Find("Vector").GetComponent<Text>().text = dataset.pos.ToString();

            dataVisu.Add(visu);
        }
    }
}
