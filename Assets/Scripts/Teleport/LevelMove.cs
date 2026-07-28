using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMove : MonoBehaviour
{
    public int sceneBuildIndex;
    public string exitPointName;
    private bool isIn;
    public GameObject popup;
    public bool showPopupOnStartOverlap = true;
    private bool playerHasLeftTrigger;
    private int overlapCount = 0;

    void Start()
    {
        if (popup != null)
        {
            popup.SetActive(false);
        }
        playerHasLeftTrigger = false;

        if (SceneManager.GetActiveScene().buildIndex == 1 && GameManager.Instance != null)
        {
            if (!string.IsNullOrEmpty(GameManager.Instance.lastExitPoint))
            {
                GameObject exitPoint = GameObject.Find(GameManager.Instance.lastExitPoint);
                if (exitPoint != null)
                {
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    if (player != null)
                    {
                        Vector3 spawnPosition = exitPoint.transform.position;
                        BoxCollider2D boxCollider = exitPoint.GetComponent<BoxCollider2D>();
                        if (boxCollider != null)
                        {
                            spawnPosition.y = exitPoint.transform.position.y -
                                (boxCollider.size.y / 2) * exitPoint.transform.lossyScale.y +
                                0.1f; // Slight offset to ensure player is above ground
                        }
                        player.transform.position = spawnPosition;
                    }
                }
                GameManager.Instance.lastExitPoint = null; // Reset the exit point
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            overlapCount++;
            if (playerHasLeftTrigger || (showPopupOnStartOverlap && overlapCount == 1))
            {
                isIn = true;
                if (popup != null)
                {
                    popup.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isIn = false;
            playerHasLeftTrigger = true;
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
            if (GameManager.Instance != null)
            {
                GameManager.Instance.lastExitPoint = exitPointName;
            }
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
        }
    }
}