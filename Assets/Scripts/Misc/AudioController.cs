using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource source;
    public enum clips { ButtonClick }
    [SerializeField] private AudioClip buttonClick;

    //SINGLETON//
    public static AudioController instance;

    private void Awake() {
        instance = this;
    }
    //END SINGLETON//

    // Start is called before the first frame update
    void Start()
    {
        source = Camera.main.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public  void PlaySound( clips clip) {
        switch (clip) {
            case clips.ButtonClick:
                source.PlayOneShot(buttonClick);
                break;
        }

    }


}
