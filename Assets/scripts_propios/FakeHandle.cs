using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandleFeedback : MonoBehaviour
{
    public AudioClip metalSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayFeedback(BaseInteractionEventArgs args)
    {
        // SONIDO
        if (metalSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(metalSound);
        }

        // VIBRACIÓN
        if (args.interactorObject is UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInputInteractor controllerInteractor)
        {
            var controller = controllerInteractor.xrController;
            if (controller != null)
            {
                controller.SendHapticImpulse(0.4f, 0.2f);
            }
        }
    }
}
