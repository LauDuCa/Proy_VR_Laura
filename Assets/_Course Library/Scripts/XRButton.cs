using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRButton_X : MonoBehaviour
{
    private Vector3 startPos;
    public float pressDepth = 0.02f; // Cuánto se mueve en X
    public float returnSpeed = 2f; // Velocidad al regresar

    private void Start()
    {
        startPos = transform.localPosition;
    }

    public void PressButton()
    {
        // Mueve el botón en el eje X
        transform.localPosition = startPos - new Vector3(pressDepth, 0, 0);
    }

    public void ReleaseButton()
    {
        // Devuelve el botón a su posición inicial suavemente
        StartCoroutine(ReturnToStart());
    }

    private System.Collections.IEnumerator ReturnToStart()
    {
        while (Vector3.Distance(transform.localPosition, startPos) > 0.001f)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, startPos, returnSpeed * Time.deltaTime);
            yield return null;
        }
        transform.localPosition = startPos;
    }
}
