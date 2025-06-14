using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        GameController.Instance.ChangeScene("SampleScene");
    }
    public void Quit()
    {
        GameController.Instance.QuitGame();
    }

    public void OpenSettings()
    {
        GameController.Instance.ChangeScene("SettingsScene");
    }
}
