using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private GameObject deathMenu;
    [SerializeField] private TextMeshProUGUI lifeText;
    private PlayerLife playerLifeScript;

     public void Start()
    {

        playerLifeScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLife>();
        
        InvokeRepeating("UpdateLifes", 0, 0.2f);
    }

    private void UpdateLifes() {
        lifeText.text = playerLifeScript.GetLife().ToString();
    }

     public void PauseGame()
     {
         if (!pauseCanvas.activeInHierarchy)
         {
             Time.timeScale = 0;
             pauseCanvas.SetActive(true);
            SoundManager.instance.Paused();
         }
         else if (pauseCanvas.activeInHierarchy)
         {
             Time.timeScale = 1.0f;
             pauseCanvas.SetActive(false);
            SoundManager.instance.UnPaused();

        }
        SoundManager.instance.Play(SoundManager.clip.pauseMenu);
     }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        pauseCanvas.SetActive(false);
        SoundManager.instance.Play(SoundManager.clip.pauseMenu);
        SoundManager.instance.UnPaused();
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        SoundManager.instance.Play(SoundManager.clip.ButtonClick);
        SoundManager.instance.UnPaused();

    }
    public void RestartLevel()
    {
        Time.timeScale = 1.0f;
        SoundManager.instance.Play(SoundManager.clip.ButtonClick);
        SoundManager.instance.UnPaused();
        Scene loadedLevel = SceneManager.GetActiveScene();
        SceneManager.LoadScene(loadedLevel.buildIndex);
    }

    public void ShowDeathMenu() {
        deathMenu.SetActive(true);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
    }
}
