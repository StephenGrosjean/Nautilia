using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public enum section { Main, Levels, Settings};

    [SerializeField] private GameObject mainMenu, settings, levels, blockScreen;
    [SerializeField] private SpriteRenderer fadeScreen;
    [SerializeField] private GameObject level_1, level_2, level_3, level_4, level_boss;
    [SerializeField] private float fadeSpeed = 0.05f;
    
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
        maxLevel = levelGetScript.Level;
        currentSection = section.Main;
        ActivateLevels();
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

    IEnumerator ChangeScene(section section) {
        blockScreen.SetActive(true);
        for(float i = 0; i < 1.1f; i += 0.05f) {
            fadeScreen.color = Color.Lerp(transparent, Color.black, i);
            yield return new WaitForSeconds(fadeSpeed);
        }
        ChangeToSection(section);
        for (float i = 0; i < 1.1f; i += 0.05f) {
            fadeScreen.color = Color.Lerp(Color.black, transparent, i);
            yield return new WaitForSeconds(fadeSpeed);
        }
        blockScreen.SetActive(false);
    }

    void ChangeToSection(section section) {
        switch (section) {
            case section.Main:
                mainMenu.SetActive(true);
                settings.SetActive(false);
                levels.SetActive(false);
                break;
            case section.Levels:
                mainMenu.SetActive(false);
                settings.SetActive(false);
                levels.SetActive(true);
                break;
            case section.Settings:
                mainMenu.SetActive(false);
                settings.SetActive(true);
                levels.SetActive(false);
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
}
