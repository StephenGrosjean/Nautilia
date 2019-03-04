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
        float vibration = (50*volume) - 50;
#if !UNITY_ANDROID
        VibrationController.Vibrate((long)vibration);
#endif
        if (volume >= 0) {
            xmlSystem.dataBase.firstDB[1].value = volume;

            xmlSystem.Save();
        }
    }

    public void ResetSlider() {
        xmlSystem.Load();
        slider.value = xmlSystem.dataBase.firstDB[1].value;

    }
}
