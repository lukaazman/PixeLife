using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject SettingsMenu;
    public GameObject Main;
    public GameObject Player;
    private bool isInSettings = false;
    public string sceneName;


    void Update()
    {
        if (isInSettings && Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape key detected, closing settings menu");
            CloseSettingsMenu();
        }
    }

    public void OpenSettings()
    {
        Debug.Log("Opening Settings Menu");
        isInSettings = true;
        Main.SetActive(false);
        SettingsMenu.SetActive(true);
        Player.SetActive(false);

        // Deselect any currently selected UI elements
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void goScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitApp()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void CloseSettingsMenu()
    {
        Main.SetActive(true);
        Player.SetActive(true);
        SettingsMenu.SetActive(false);
        isInSettings = false;
    }
}
