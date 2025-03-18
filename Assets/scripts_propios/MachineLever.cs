using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class MachineLever : MonoBehaviour
{
    public Transform leverVisual; // Parte de la palanca que rota
    public float rotationAngle = 30f; // Ángulo de rotación
    public float returnSpeed = 2f; // Velocidad de retorno
    public AudioSource leverSound; // Sonido de la palanca

    private Quaternion originalRotation;
    private bool isActivated = false;

    void Start()
    {
        if (leverVisual == null)
            leverVisual = transform;

        originalRotation = leverVisual.localRotation;
    }

    public void ToggleLever()
    {
        if (leverSound != null)
            leverSound.Play(); // Reproducir sonido

        isActivated = !isActivated;
        StopAllCoroutines();
        StartCoroutine(RotateLever(isActivated ? Quaternion.Euler(-rotationAngle, 0, 0) * originalRotation : originalRotation));
    }

    private IEnumerator RotateLever(Quaternion targetRotation)
    {
        while (Quaternion.Angle(leverVisual.localRotation, targetRotation) > 0.1f)
        {
            leverVisual.localRotation = Quaternion.Slerp(leverVisual.localRotation, targetRotation, Time.deltaTime * returnSpeed);
            yield return null;
        }
        leverVisual.localRotation = targetRotation;
    }
}