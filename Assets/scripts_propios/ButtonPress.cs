using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class ButtonPress : MonoBehaviour
{
    private Vector3 originalPosition;
    public float pressDepth = 0.02f; // Cuánto se hunde el botón al presionarlo
    public float returnSpeed = 5f; // Velocidad al volver a su posición original
    public AudioSource buttonSound; // Sonido del botón

    void Start()
    {
        originalPosition = transform.localPosition; // Guarda la posición inicial del botón
    }

    public void PressButton()
    {
        if (buttonSound != null)
            buttonSound.Play(); // Reproduce sonido al presionar

        StopAllCoroutines(); // Detener animaciones previas si hay
        StartCoroutine(PressAnimation()); // Iniciar animación del botón
    }

    private IEnumerator PressAnimation()
    {
        // 👇 ESTA LÍNEA GARANTIZA QUE SE MUEVA HACIA ATRÁS EN SU EJE LOCAL 👇
        transform.localPosition = originalPosition + transform.up * -pressDepth; // Prueba en el eje Y local
        yield return new WaitForSeconds(0.1f); // Pequeña pausa

        // Regresa el botón a su posición original suavemente
        while (Vector3.Distance(transform.localPosition, originalPosition) > 0.001f)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalPosition, Time.deltaTime * returnSpeed);
            yield return null;
        }
        transform.localPosition = originalPosition; // Asegura que vuelva a su lugar exacto
    }
}