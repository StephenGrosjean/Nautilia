using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject pauseCanvas;

    private void Start()
    {
        
    }

    private void Update()
     {
         
     }

     public void StartGame()
    {
        //SceneManager.LoadScene("EthanScene");
    }

     public void PauseGame()
     {
         if (!pauseCanvas.activeInHierarchy)
         {
             Time.timeScale = 0;
             pauseCanvas.SetActive(true);
         }
         else if (pauseCanvas.activeInHierarchy)
         {
             Time.timeScale = 1.0f;
             pauseCanvas.SetActive(false);
         }
        SoundManager.instance.Play(SoundManager.clip.pauseMenu);
     }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        pauseCanvas.SetActive(false);
        SoundManager.instance.Play(SoundManager.clip.pauseMenu);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        SoundManager.instance.Play(SoundManager.clip.ButtonClick);
    }
    public void RestartLevel()
    {
        Scene loadedLevel = SceneManager.GetActiveScene();
        SceneManager.LoadScene(loadedLevel.buildIndex);
        Time.timeScale = 1.0f;
        SoundManager.instance.Play(SoundManager.clip.ButtonClick);
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
