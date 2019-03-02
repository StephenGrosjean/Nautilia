using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelGet : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private XMLSave saveScript;
    private int level;

    // Start is called before the first frame update
    void Start() {
        saveScript.Load();

        foreach (FirstDataset dataset in saveScript.dataBase.firstDB) {
            if (dataset.name == "Level") {
                level = dataset.value;
            }

        }

        text.text = "Level : " + level.ToString();
    }

}
