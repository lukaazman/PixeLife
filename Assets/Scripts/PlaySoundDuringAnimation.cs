using UnityEngine;

public class PlaySoundDuringAnimation : MonoBehaviour
{
    public AudioSource audioSource;

    public void PlaySound()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
