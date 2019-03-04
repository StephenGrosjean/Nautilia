using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    //SINGLETON//
    public static SoundManager instance;

    private void Awake() {
        instance = this;
    }
    //END SINGLETON//

    public enum clip { ButtonClick, PlayerHit, enemyDeath, bossHit, pauseMenu};
    [SerializeField] private AudioClip buttonClick, playerHit, enemyDeath, bossHit, pauseMenu;

    private AudioSource source;

    void Start()
    {
        source = Camera.main.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        }
    }

}
