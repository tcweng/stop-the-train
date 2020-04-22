using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class ButtonManager : MonoBehaviour
{
    public void StartGame()
    {
        // Go to Game Scene
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        // Go to Main Scene
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        // Quit the App
        Application.Quit();
    }

    public void Pause()
    {
        // Pause the game
        Time.timeScale = 0;
    }

    public void Resume()
    {
        // Resume the game
        Time.timeScale = 1;
    }
}
