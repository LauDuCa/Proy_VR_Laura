using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class MachineButton : MonoBehaviour
{
    public Transform visualTransform; // El objeto que se moverá
    public float pressDepth = 0.02f; // Cuánto se hunde el botón
    public float returnSpeed = 5f; // Velocidad de retorno
    public AudioSource buttonSound; // Sonido del botón

    private Vector3 originalPosition;

    void Start()
    {
        if (visualTransform == null)
            visualTransform = transform;

        originalPosition = visualTransform.localPosition;
    }

    public void PressButton()
    {
        if (buttonSound != null)
            buttonSound.Play(); // Reproduce sonido

        StopAllCoroutines();
        StartCoroutine(PressAnimation());
    }

    private IEnumerator PressAnimation()
    {
        // Mueve el botón hacia adentro
        visualTransform.localPosition = originalPosition + new Vector3(-pressDepth, 0, 0);
        yield return new WaitForSeconds(0.1f);

        // Devuelve el botón a su posición original
        while (Vector3.Distance(visualTransform.localPosition, originalPosition) > 0.001f)
        {
            visualTransform.localPosition = Vector3.Lerp(visualTransform.localPosition, originalPosition, Time.deltaTime * returnSpeed);
            yield return null;
        }
        visualTransform.localPosition = originalPosition;
    }
}