using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    [SerializeField] private SpriteRenderer fadeObject;
    [SerializeField] private GameObject text;
    [SerializeField] private float fadeSpeed = 0.05f;
    [SerializeField] private float blinkSpeed;

    private Color transparent = new Color(0, 0, 0, 0);

    void Start() {
        StartCoroutine(Blink());
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        for (float i = 0; i < 1.1f; i += 0.05f) {
            fadeObject.color = Color.Lerp(Color.black, transparent, i);
            yield return new WaitForSeconds(fadeSpeed);
        }
    }
    public void Continue() {
        SoundManager.instance.Play(SoundManager.clip.ButtonClick);
        if (!Application.isEditor) {
            VibrationController.Vibrate(50);
        }
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn() {
        for (float i = 0; i < 1.1f; i += 0.05f) {
            fadeObject.color = Color.Lerp(transparent, Color.black, i);
            yield return new WaitForSeconds(fadeSpeed);
        }
        SceneManager.LoadScene("MainMenu");
    }
    IEnumerator Blink() {
        while (true) {
            yield return new WaitForSeconds(blinkSpeed);
            text.SetActive(false);
            yield return new WaitForSeconds(blinkSpeed);
            text.SetActive(true);
        }
    }
}
