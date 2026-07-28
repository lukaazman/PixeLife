using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportManager : MonoBehaviour
{
    public static TeleportManager Instance;

    public string lastExitSpawnPoint;  // Name of the last exit point
    public GameObject player;  // Reference to the player object

    private bool isIn;
    public int sceneBuildIndex;
    public GameObject popup;
    public string spawnPointName;  // Name of the current spawn point for this teleport location

    private void Awake()
    {
        // Ensure this object persists between scenes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist this object between scene changes
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
        }
    }

    void Start()
    {
        if (popup != null)
        {
            popup.SetActive(false);
        }

        // If there's a spawn point stored, move the player to that location at scene start
        if (!string.IsNullOrEmpty(lastExitSpawnPoint))
        {
            GameObject targetSpawnPoint = GameObject.Find(lastExitSpawnPoint);
            if (targetSpawnPoint != null && player != null)
            {
                player.transform.position = targetSpawnPoint.transform.position;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isIn = true;
            if (popup != null)
            {
                popup.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isIn = false;
            if (popup != null)
            {
                popup.SetActive(false);
            }
        }
    }

    void Update()
    {
        if (isIn && Input.GetKeyDown(KeyCode.E))
        {
            // Save the current spawn point before switching scene
            if (!string.IsNullOrEmpty(spawnPointName))
            {
                lastExitSpawnPoint = spawnPointName;
            }

            // Load the next scene
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
        }
    }
}
