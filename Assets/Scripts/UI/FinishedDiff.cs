using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/// <summary>
/// CHeck if boss has been completed in difficulty
/// </summary>
public class FinishedDiff : MonoBehaviour
{
    [SerializeField] private int difficulty;

    private float completed;
    private TextMeshProUGUI text;

    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        XMLSave.instance.Load();
        completed = XMLSave.instance.dataBase.firstDB[8 + difficulty].value;
    }

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
