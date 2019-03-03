using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private XMLSave xmlSystem;
    private Slider slider;


    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = xmlSystem.dataBase.firstDB[1].value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RegisterAudio() {
        float volume = slider.value;
        if (volume >= 0) {
            xmlSystem.dataBase.firstDB[1].value = volume;

            xmlSystem.Save();
        }
    }

    public void UpdateVolume() {
            slider.value = xmlSystem.dataBase.firstDB[1].value;
        
    }
}
