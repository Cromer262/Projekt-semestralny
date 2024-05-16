using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Game"); // Завантажуємо сцену гри
    }

    public void QuitGame()
    {
        Application.Quit(); // Вимикаємо додаток
    }
}
