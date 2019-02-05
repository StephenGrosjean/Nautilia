using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    public static int scoreValue;

    private TMP_Text score;
    private void Start()
    {
        score = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        score.text = "Score" + scoreValue;
    }
}
