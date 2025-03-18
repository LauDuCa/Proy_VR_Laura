using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class CabinetDoor : MonoBehaviour
{
    public float openAngle = 100f; // Ángulo de apertura
    public float openSpeed = 3f; // Velocidad de apertura
    public AudioSource doorSound; // Sonido de la puerta
    public AudioClip openClip; // Sonido al abrir
    public AudioClip closeClip; // Sonido al cerrar

    private bool isOpen = false; // Estado de la puerta
    private Quaternion closedRotation; // Rotación cerrada
    private Quaternion openRotation; // Rotación abierta

    void Start()
    {
        closedRotation = transform.localRotation;
        openRotation = closedRotation * Quaternion.Euler(0, openAngle, 0);
    }

    public void ToggleDoor()
    {
        isOpen = !isOpen;

        // Reproducir sonido según el estado de la puerta
        if (doorSound != null)
        {
            doorSound.clip = isOpen ? openClip : closeClip;
            doorSound.Play();
        }

        StopAllCoroutines();
        StartCoroutine(RotateDoor(isOpen ? openRotation : closedRotation));
    }

    private IEnumerator RotateDoor(Quaternion targetRotation)
    {
        while (Quaternion.Angle(transform.localRotation, targetRotation) > 0.1f)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, Time.deltaTime * openSpeed);
            yield return null;
        }
        transform.localRotation = targetRotation;
    }
}
