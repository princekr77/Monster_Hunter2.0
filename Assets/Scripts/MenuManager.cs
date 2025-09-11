using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    public GameObject exitUI;
    public GameObject menuUI;
    public GameObject loadingScreen;
    public Slider loadingBar;

    public float stepDelay = 0.025f; // controls speed of fake bar

    // Play button clicked
    public void PlayGame()
    {
        menuUI.SetActive(false);
        exitUI.SetActive(false);
        loadingScreen.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        StartCoroutine(LoadSceneWithFakeBar("My terrain"));
    }

    IEnumerator LoadSceneWithFakeBar(string sceneName)
    {
        loadingBar.value = 0;

        // Start loading scene in background
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false; // hold scene until bar is ready

        // Fake bar progress
        while (loadingBar.value < 100)
        {
            loadingBar.value += 1.5f; // increase smoothly (5% each step if you want)
            yield return new WaitForSeconds(stepDelay);

            // If the scene is almost done loading, and our bar is filled → break
            if (operation.progress >= 90 && loadingBar.value >= 100)
            {
                break;
            }
        }

        // small delay so user sees bar at 100%
        yield return new WaitForSeconds(0.5f);

        // Now activate scene
        operation.allowSceneActivation = true;
    }

    public void ExitButton()
    {
        menuUI.SetActive(false);
        exitUI.SetActive(true);
    }

    public void Back()
    {
        exitUI.SetActive(false);
        menuUI.SetActive(true);
    }

    public void Exit()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }
}
