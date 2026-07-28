using Ink.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    public int sceneBuildIndex;

    private bool playerInRange;

    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }

    private void Update()
    {
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            visualCue.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E) || InputManager.GetInstance().GetInteractPressed())
            {
                StartDialogue();
            }
        }
        else
        {
            visualCue.SetActive(false);
        }
    }

    private void StartDialogue()
    {
        var dialogueManager = DialogueManager.GetInstance();
        dialogueManager.EnterDialogue(inkJSON);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }

    public void goToArena()
    {
        SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
    }
}
