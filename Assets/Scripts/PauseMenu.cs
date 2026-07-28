using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public string SceneName;

    private GameObject arrivaMapUI; // Private reference to store the Arriva Map object

    void Start()
    {
        // Dynamically find the ArrivaMap object by tag or name at the start
        arrivaMapUI = GameObject.FindWithTag("Arriva"); // Assuming you have tagged the ArrivaMap with "Arriva"
        // If using the name instead of tag, you can use:
        // arrivaMapUI = GameObject.Find("Canvas2"); // Replace with your exact GameObject name
    }

    void Update()
    {
        // If Arriva Map exists and is open, don't allow Pause Menu to open
        if (arrivaMapUI != null && arrivaMapUI.activeSelf)
            return;  // Skip pause menu logic if the Arriva Map is active

        // Handle Pause Menu toggle
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
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(SceneName);
        Resume();
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
