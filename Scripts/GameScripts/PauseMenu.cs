using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenu;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        //freeses game
        GameIsPaused = false;
    }

    private void Pause()
    {
        pauseMenu.SetActive(true);
        //freeses game
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Debug.Log("Load Menu");
        LevelLoader.LoadLevel(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
