using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public enum section { Main, Levels, Settings};

    [SerializeField] private GameObject mainMenu, settings, levels, blockScreen;
    [SerializeField] private SpriteRenderer fadeScreen;
    [SerializeField] private GameObject level_1, level_2, level_3, level_4, level_boss;
    [SerializeField] private float fadeSpeed = 0.05f;
    [SerializeField] private TextMeshProUGUI sectionIndicator;
    
    private Color transparent = new Color(0, 0, 0, 0);
    private section currentSection;
    private bool canClick = true;
    private LevelGet levelGetScript;
    private int maxLevel;

    // Start is called before the first frame update
    private void Awake() {
        levelGetScript = GetComponent<LevelGet>();
    }

    void Start()
    {
        StartCoroutine(FadeOut());
        maxLevel = levelGetScript.Level;
        currentSection = section.Main;
        ActivateLevels();
        
    }
    IEnumerator FadeOut() {
        for (float i = 0; i < 1.1f; i += 0.05f) {
            fadeScreen.color = Color.Lerp(Color.black, transparent, i);
            yield return new WaitForSeconds(fadeSpeed);
        }
    }

    // Update is called once per frame
    void Update()
    {
        maxLevel = levelGetScript.Level;
    }

    public void Goto_Levels() {
        StartCoroutine(ChangeScene(section.Levels));
    }

    public void Goto_Settings() {
        StartCoroutine(ChangeScene(section.Settings));
    }

    public void Return_MainMenu() {
        StartCoroutine(ChangeScene(section.Main));
    }

    public void LoadLevel1() {
        SceneManager.LoadScene("Level1");
    }
    public void LoadLevel2() {
        SceneManager.LoadScene("Level2");
    }
    public void LoadLevel3() {
        SceneManager.LoadScene("Level3");
    }
    public void LoadLevel4() {
        SceneManager.LoadScene("Level4");
    }
    public void LoadLevel5() {
        SceneManager.LoadScene("Level5");
    }


    IEnumerator ChangeScene(section section) {
        SoundManager.instance.Play(SoundManager.clip.ButtonClick);
        if (!Application.isEditor) {
            VibrationController.Vibrate(50);
        }

        blockScreen.SetActive(true);
        for(float i = 0; i < 1.1f; i += 0.05f) {
            fadeScreen.color = Color.Lerp(transparent, Color.black, i);
            yield return new WaitForSeconds(fadeSpeed);
        }
        ChangeToSection(section);
        setIndicatorText();
        
        for (float i = 0; i < 1.1f; i += 0.05f) {
            fadeScreen.color = Color.Lerp(Color.black, transparent, i);
            yield return new WaitForSeconds(fadeSpeed);
            if(i > 0.8f) {
                blockScreen.SetActive(false);
            }
        }
    }

    void ChangeToSection(section section) {
        switch (section) {
            case section.Main:
                mainMenu.SetActive(true);
                settings.SetActive(false);
                levels.SetActive(false);
                currentSection = section.Main;
                break;
            case section.Levels:
                mainMenu.SetActive(false);
                settings.SetActive(false);
                levels.SetActive(true);
                currentSection = section.Levels;
                break;
            case section.Settings:
                mainMenu.SetActive(false);
                settings.SetActive(true);
                levels.SetActive(false);
                currentSection = section.Settings;
                break;
        }
    }

    void ActivateLevels() {
        switch (maxLevel) {
            case 1:
                level_1.SetActive(true);
                level_2.SetActive(true);
                break;
            case 2:
                level_1.SetActive(true);
                level_2.SetActive(true);
                level_3.SetActive(true);
                break;
            case 3:
                level_1.SetActive(true);
                level_2.SetActive(true);
                level_3.SetActive(true);
                level_4.SetActive(true);
                break;
            case 4:
                level_1.SetActive(true);
                level_2.SetActive(true);
                level_3.SetActive(true);
                level_4.SetActive(true);
                level_boss.SetActive(true);
                break;
        }
    }

    void setIndicatorText() {
        switch (currentSection) {
            case section.Main:
                sectionIndicator.text = "Main Menu";
                break;
            case section.Settings:
                sectionIndicator.text = "Settings";
                break;
            case section.Levels:
                sectionIndicator.text = "Levels";
                break;
        }
    }
}
