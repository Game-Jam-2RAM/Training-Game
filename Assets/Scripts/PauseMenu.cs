using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    // Pause the game
    public static bool Paused = false;
    public GameObject PauseCanvas;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f; // Ensure the game starts unpaused

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Paused)
            {
                Play();
            }
            else
            {
                Stop();
            }
        }
    }

    void Stop()
    {
        PauseCanvas.SetActive(true);
        Time.timeScale = 0f; // Pause the game
        Paused = true;
        Debug.Log("Game paused.");
    }

    public void Play()
    {
        PauseCanvas.SetActive(false);
        Time.timeScale = 1f; // Resume the game
        Paused = false;
        Debug.Log("Game resumed.");
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
