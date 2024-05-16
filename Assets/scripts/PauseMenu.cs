using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;

    private bool isPaused = false;
    private bool previousCursorState;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
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
        if (!isPaused)
            return;

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Pause()
    {
        if (isPaused)
            return;

        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        previousCursorState = Cursor.visible;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public bool IsPaused
    {
        get { return isPaused; }
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
