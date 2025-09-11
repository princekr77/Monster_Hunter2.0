using UnityEngine;
using UnityEngine.SceneManagement; // Needed for Restart

public class UIManager : MonoBehaviour
{
    public GameObject pauseMenuUI;// Assign Pause Menu Panel in Inspector
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
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

    void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        // Keep cursor visible & free
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        // Hide cursor again after resuming (FPS style)
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // 🔹 Restart the current scene
    public void RestartGame()
    {
        Time.timeScale = 1f; // Ensure time runs again
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // 🔹 Quit the game
    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        SceneManager.LoadSceneAsync("Menu");
    }

    public void Menu()
    {
        SceneManager.LoadSceneAsync("Menu");
    }
}
