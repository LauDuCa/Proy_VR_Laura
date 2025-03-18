using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FolderSound : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip grabSound;   // Sonido al coger la carpeta
    public AudioClip dropSound;   // Sonido al soltarla

    private Rigidbody rb;
    private bool hasLanded = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    // Sonido al agarrar la carpeta (sin parámetros)
    public void PlayGrabSound()
    {
        if (grabSound != null)
        {
            audioSource.PlayOneShot(grabSound);
        }
    }

    // Detectar cuándo la carpeta cae al suelo
    void OnCollisionEnter(Collision collision)
    {
        if (!hasLanded && dropSound != null)
        {
            audioSource.PlayOneShot(dropSound);
            hasLanded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        hasLanded = false;
    }
}
