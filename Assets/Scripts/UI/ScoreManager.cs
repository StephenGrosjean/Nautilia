using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    public static int scoreValue;

    private TMP_Text score;

    private int prevScore;

    private void Start()
    {
        score = GetComponent<TMP_Text>();
        score.text = "Score : " + scoreValue;
    }

    private void Update()
    {
        if(prevScore != scoreValue) {
            UpdateUI();
        }
        prevScore = scoreValue;
    }

    public static void AddScore(int value) {
        scoreValue += value;
    }

    void UpdateUI() {
        score.text = "Score : " + scoreValue;
    }

    
}
