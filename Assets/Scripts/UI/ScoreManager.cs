using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static int scoreValue;

    private TMP_Text score;

    private int prevScore;
    private int levelID;
    private float maxScore1, maxScore2, maxScore3, maxScore4, maxScore5;
    


    private void Start()
    {
        scoreValue = 0;
        score = GetComponent<TMP_Text>();
        score.text = scoreValue.ToString();
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("BossScene")) {
            levelID = BossGameManager.instance.LevelID;
        }
        else {
            levelID = EnemySpawnSystem.instance.LevelID;
        }

        maxScore1 = XMLSave.instance.dataBase.firstDB[2].value;
        maxScore2 = XMLSave.instance.dataBase.firstDB[3].value;
        maxScore3 = XMLSave.instance.dataBase.firstDB[4].value;
        maxScore4 = XMLSave.instance.dataBase.firstDB[5].value;
        maxScore5 = XMLSave.instance.dataBase.firstDB[6].value;


    }

    private void Update()
    {
        if (prevScore != scoreValue)
        {
            UpdateUI();
            SaveScore();
        }

        prevScore = scoreValue;
    }

    public static void AddScore(int value)
    {
        scoreValue += value;
    }

    void UpdateUI()
    {
        score.text = scoreValue.ToString();
    }

    void SaveScore() {
        switch (levelID) {
            case 1:
                if (scoreValue > maxScore1) {
                    Debug.Log("HI");
                    XMLSave.instance.dataBase.firstDB[2].value = scoreValue;
                    XMLSave.instance.Save();
                }
                break;
            case 2:
                if (scoreValue > maxScore2) {
                    XMLSave.instance.dataBase.firstDB[3].value = scoreValue;
                    XMLSave.instance.Save();
                }
                break;
            case 3:
                if (scoreValue > maxScore3) {
                    XMLSave.instance.dataBase.firstDB[4].value = scoreValue;
                    XMLSave.instance.Save();
                }
                break;
            case 4:
                if (scoreValue > maxScore4) {
                    XMLSave.instance.dataBase.firstDB[5].value = scoreValue;
                    XMLSave.instance.Save();
                }
                break;
            case 5:
                if (scoreValue > maxScore5) {
                    XMLSave.instance.dataBase.firstDB[6].value = scoreValue;
                    XMLSave.instance.Save();
                }
                break;
        }
    }
}
