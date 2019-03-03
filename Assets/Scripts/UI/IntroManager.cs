using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer fadeScreen;
    [SerializeField] private GameObject text;

    private Color transparent = new Color(0, 0, 0, 0);
    [SerializeField] private float fadeSpeed = 0.05f;
    [SerializeField] private float blinkSpeed;

    void Start()
    {
        StartCoroutine(Blink());
        StartCoroutine(First());
    }


    public void Continue() {

        AudioController.instance.PlaySound(AudioController.clips.ButtonClick);

        StartCoroutine(ContinueToMenu());
    }

    IEnumerator First() {
        for (float i = 0; i < 1.1f; i += 0.05f) {
            fadeScreen.color = Color.Lerp(Color.black, transparent, i);
            yield return new WaitForSeconds(fadeSpeed);
        }
    }

     IEnumerator ContinueToMenu() {
        for (float i = 0; i < 1.1f; i += 0.05f) {
            fadeScreen.color = Color.Lerp(transparent, Color.black, i);
            yield return new WaitForSeconds(fadeSpeed);
        }

        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator Blink() {
        while (true) {
            text.SetActive(false);
            yield return new WaitForSeconds(blinkSpeed);
            text.SetActive(true);
            yield return new WaitForSeconds(blinkSpeed);
        }
    }
}
