using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // This script is for controlling sences, background music, and other game-wide settings.
    // It can also be used to manage game state, such as starting, pausing, and ending the game.

    public static GameController Instance;

    [Header("Audio")]
    public AudioSource musicSource;
    public AudioClip backgroundMusic;
    public int musicVolume = 100;
    private void Awake()
    {
        // Singleton logic
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        PlayBackgroundMusic();
    }

    void PlayBackgroundMusic()
    {
        if (musicSource && backgroundMusic)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.volume = musicVolume / 100f;
            musicSource.Play();
        }
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetMusicVolume(int volume)
    {
        musicVolume = volume;
        if (musicSource)
            musicSource.volume = volume / 100f;
    }
}
