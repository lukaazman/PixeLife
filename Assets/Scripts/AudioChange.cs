using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    public Button muteButton;  // Reference to the Button component
    public Image imageComponent;  // Reference to the Image component you want to change
    public Sprite volumeOnSprite;  // Reference to the Volume-Sheet_0 sprite
    public Sprite volumeOffSprite; // Reference to the Volume-Sheet1 sprite

    private bool isMuted = false;

    void Start()
    {
        // Ensure that the button and image are assigned in the inspector
        if (muteButton != null)
        {
            muteButton.onClick.AddListener(ToggleMute);
        }

        // Initialize with the correct sprite
        if (imageComponent != null && !isMuted)
        {
            imageComponent.sprite = volumeOnSprite;
        }
    }

    void ToggleMute()
    {
        isMuted = !isMuted;

        // Mute or unmute the game audio
        AudioListener.volume = isMuted ? 0 : 1;

        // Change the sprite based on the mute state
        if (imageComponent != null)
        {
            imageComponent.sprite = isMuted ? volumeOffSprite : volumeOnSprite;
        }
    }
}
