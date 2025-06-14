using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    public Slider musicVolumeSlider;
    public void Start()
    {   
        musicVolumeSlider.value = GameController.Instance.musicVolume / 100f;
    }
    public void CloseSettings()
    {
        GameController.Instance.ChangeScene("MainMenu");
    }

    public void SetMusicVolume(Slider slider)
    {
        GameController.Instance.SetMusicVolume((int)(slider.value * 100));
    }
}
