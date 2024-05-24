using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
        else
        {
            Debug.LogError("Pause menu is not assigned in the inspector!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        Debug.Log("Game Paused");
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Debug.LogError("Pause menu is not assigned in the inspector!");
        }
    }

    public void ResumeGame()
    {
        Debug.Log("Game Resumed");
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Debug.LogError("Pause menu is not assigned in the inspector!");
        }
    }

    public void GoToMainMenu()
    {
        Debug.Log("Going to Main Menu");
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}