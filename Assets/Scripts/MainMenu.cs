using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Start game
    public void Play()
    {
       // SceneManager.LoadScene("SampleScene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("Player has start the game.");
    }
    //Exit game
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Player has quit the game.");
    }
}
