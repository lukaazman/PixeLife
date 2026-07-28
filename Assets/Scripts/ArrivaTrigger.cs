using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrivaTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;
    private bool CanGoBus;
    public GameObject ArrivaMap;
    public GameObject AlreadyHere;

    private MojMovement mojMovement;

    private void Start()
    {
        visualCue.SetActive(false);
        CanGoBus = false;

        // Safely find and assign the MojMovement component
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            mojMovement = player.GetComponent<MojMovement>();
        }

        // Check if the mojMovement script was found
        if (mojMovement == null)
        {
            Debug.LogError("MojMovement script not found on the Player object!");
        }
    }

    private void Update()
    {
        // Open the Arriva Map if "E" is pressed and the map is not already open
        if (CanGoBus && Input.GetKeyDown(KeyCode.E))
        {
            ArrivaMap.SetActive(true);
            AlreadyHere.SetActive(false);
            Time.timeScale = 0f;

            // Stop both the walk and jump sounds when the map is opened
            if (mojMovement != null)
            {
                mojMovement.StopWalkSound();
                mojMovement.StopJumpSound();
            }

            // Disable PauseMenu functionality when Arriva Map is open
            PauseMenu.GameIsPaused = true; // This temporarily blocks the Pause Menu while Arriva Map is open
        }

        if (ArrivaMap.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            ArrivaMap.SetActive(false);

            if (AlreadyHere != null)
            {
                AlreadyHere.SetActive(false);
            }

            Time.timeScale = 1f;
            PauseMenu.GameIsPaused = false;  // Re-enable PauseMenu
        }

    }



        private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            visualCue.SetActive(true);
            CanGoBus = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            visualCue.SetActive(false);
            CanGoBus = false;
        }
    }
    void ActivateArrivaMap()
    {
        StartCoroutine(ActivateMapDelayed());
    }

    IEnumerator ActivateMapDelayed()
    {
        yield return null; // Wait for the next frame
        ArrivaMap.SetActive(true);
    }

}
