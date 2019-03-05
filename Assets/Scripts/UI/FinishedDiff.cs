using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinishedDiff : MonoBehaviour
{
    [SerializeField] private int difficulty;

    private float completed;
    private TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        XMLSave.instance.Load();
        completed = XMLSave.instance.dataBase.firstDB[8 + difficulty].value;
    }

    // Update is called once per frame
    void Update()
    {
        if(completed == 1) {
            text.text = "YES";
            text.color = Color.green;
        }
        else {
            text.text = "NO";
            text.color = Color.red;
        }
    }
}
