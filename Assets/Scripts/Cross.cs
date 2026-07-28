using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cross : MonoBehaviour
{
    public GameObject popup;

    void Start()
    {
        // Find the GameObject with the "Popup" tag and hide it at the start
        if (popup != null)
        {
            popup.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Show the popup when the player enters the trigger zone
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
            // Hide the popup when the player exits the trigger zone
            if (popup != null)
            {
                popup.SetActive(false);
            }
        }
    }
}
