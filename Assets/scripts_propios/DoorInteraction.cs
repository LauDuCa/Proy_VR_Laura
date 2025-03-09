using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorInteraction : MonoBehaviour
{
    public Transform doorPivot; // Punto de rotación de la puerta
    public float openAngle = 90f; // Ángulo de apertura
    public float openSpeed = 2f; // Velocidad de apertura
    public AudioSource doorSound; // Sonido de la puerta
    public AudioClip openClip; // Sonido al abrir
    public AudioClip closeClip; // Sonido al cerrar

    private bool isOpen = false; // Estado de la puerta
    private Quaternion closedRotation; // Rotación original
    private Quaternion openRotation; // Rotación abierta

    void Start()
    {
        closedRotation = doorPivot.rotation;
        openRotation = closedRotation * Quaternion.Euler(0, openAngle, 0);
        
        // Si no has asignado el AudioSource en Unity, lo busca automáticamente
        if (doorSound == null)
            doorSound = GetComponent<AudioSource>();
    }

    public void ToggleDoor()
    {
        isOpen = !isOpen;

        // Reproduce sonido según el estado de la puerta
        if (doorSound != null)
        {
            doorSound.clip = isOpen ? openClip : closeClip;
            doorSound.Play();
        }

        StopAllCoroutines();
        StartCoroutine(RotateDoor(isOpen ? openRotation : closedRotation));
    }

    private System.Collections.IEnumerator RotateDoor(Quaternion targetRotation)
    {
        while (Quaternion.Angle(doorPivot.rotation, targetRotation) > 0.1f)
        {
            doorPivot.rotation = Quaternion.Slerp(doorPivot.rotation, targetRotation, Time.deltaTime * openSpeed);
            yield return null;
        }
        doorPivot.rotation = targetRotation;
    }
}