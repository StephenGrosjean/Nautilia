using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private XMLSave xmlSystem;
    private Slider slider;


    void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = xmlSystem.dataBase.firstDB[1].value;
    }


    public void RegisterAudio() {
        float volume = slider.value;
        float vibration = (130*volume);
        if (!Application.isEditor) {
            VibrationController.Vibrate((long)vibration);
        }
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
