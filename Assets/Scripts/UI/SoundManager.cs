using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour {

    //SINGLETON//
    public static SoundManager instance;

    private void Awake() {
        instance = this;
    }
    //END SINGLETON//

    public enum clip { ButtonClick, PlayerHit, enemyDeath, bossHit, pauseMenu, upgrade};
    [SerializeField] private AudioClip buttonClick, playerHit, enemyDeath, bossHit, pauseMenu, upgrade;
    [SerializeField] private AudioMixerSnapshot normal, paused;
    [SerializeField] private AudioMixer mixer;

    private AudioSource source;
    private float volume;

    void Start()
    {
        XMLSave.instance.Load();
        normal.TransitionTo(0.2f);
        source = Camera.main.transform.Find("FX").GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        volume = XMLSave.instance.dataBase.firstDB[1].value;
        mixer.SetFloat("volume", -80+(160*volume)-(80*Mathf.Pow(volume,2)));
    }

    public void Play(clip clip) {
        switch (clip) {
            case clip.ButtonClick:
                source.PlayOneShot(buttonClick);
                break;
            case clip.PlayerHit:
                source.PlayOneShot(playerHit);
                break;
            case clip.enemyDeath:
                source.PlayOneShot(enemyDeath);
                break;
            case clip.bossHit:
                source.PlayOneShot(bossHit);
                break;
            case clip.pauseMenu:
                source.PlayOneShot(pauseMenu);
                break;
            case clip.upgrade:
                source.PlayOneShot(upgrade);
                break;
        }
    }

    public void Paused() {
        paused.TransitionTo(0.2f);
    }

    public void UnPaused() {
        normal.TransitionTo(0.2f);
    }
}
