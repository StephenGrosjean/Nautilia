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
         PauseGame();
     }

     public void StartGame()
    {
        SceneManager.LoadScene("EthanScene");
    }
    
    private void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!pauseCanvas.activeInHierarchy)
            {
                Time.timeScale = 0;
                pauseCanvas.SetActive(true);
            }
            else if(pauseCanvas.activeInHierarchy)
            {
                Time.timeScale = 1.0f;
                pauseCanvas.SetActive(false);
            }

        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        pauseCanvas.SetActive(false);
    }
    
    public void LoadLevelSelectMenu()
    {
        SceneManager.LoadScene("LevelSelectScene");
    }
    public void RestartLevel()
    {
        Scene loadedLevel = SceneManager.GetActiveScene();
        SceneManager.LoadScene(loadedLevel.buildIndex);
        Time.timeScale = 1.0f;
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
