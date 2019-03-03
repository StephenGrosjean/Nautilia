using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelGet : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private XMLSave saveScript;
    private int level;
    public int Level {
        get { return level; }
    }

    // Start is called before the first frame update
    void Awake() {
        saveScript.Load();

        foreach (FirstDataset dataset in saveScript.dataBase.firstDB) {
            if (dataset.name == "Level") {
                level = (int)dataset.value;
            }
        }

    }

    public void ResetSettings() {
        saveScript.dataBase.firstDB[1].value = 1;
        saveScript.Save();
    }

    public void ResetGame() {
        saveScript.dataBase.firstDB[0].value = 0;
        saveScript.Save();
    }

    //text.text = "Level : " + level.ToString();
}


