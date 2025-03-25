using UnityEngine;

public class StuckKeySoundFromSlot : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip stuckSound;

    public void PlayStuckSound()
    {
        if (audioSource != null && stuckSound != null)
        {
            audioSource.PlayOneShot(stuckSound);
        }
    }
}