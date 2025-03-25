using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class BreakKeySnap : MonoBehaviour
{
    public string keyTag = "Key";
    public Transform insertPoint;
    public AudioClip breakSound;
    public float snapSpeed = 5f;
    public float delayBeforeLock = 1f;
    public float finalTurnAngle = 30f;
    public float turnSpeed = 3f;

    private AudioSource audioSource;
    private bool isBreaking = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isBreaking || !other.CompareTag(keyTag)) return;

        isBreaking = true;

        XRGrabInteractable grab = other.GetComponent<XRGrabInteractable>();
        if (grab != null)
        {
            grab.enabled = false; // Desactiva el grab inmediatamente
            Collider grabCollider = grab.GetComponent<Collider>();
            if (grabCollider != null)
            {
                grabCollider.enabled = false;
            }
        }

        StartCoroutine(SnapAndTurn(other.gameObject, grab));
    }

    private System.Collections.IEnumerator SnapAndTurn(GameObject key, XRGrabInteractable grab)
    {
        float t = 0f;
        Vector3 startPos = key.transform.position;
        Quaternion startRot = key.transform.rotation;

        // 🧲 Movimiento hacia la cerradura
        while (t < 1f)
        {
            t += Time.deltaTime * snapSpeed;
            key.transform.position = Vector3.Lerp(startPos, insertPoint.position, t);
            key.transform.rotation = Quaternion.Slerp(startRot, insertPoint.rotation, t);
            yield return null;
        }

        // Forzar la posición final exacta
        key.transform.position = insertPoint.position;
        key.transform.rotation = insertPoint.rotation;

        yield return new WaitForSeconds(0.2f);

        // 🔄 Giro final simulando intento de abrir
        Quaternion originalRotation = key.transform.rotation;
        Quaternion finalRotation = originalRotation * Quaternion.Euler(0, finalTurnAngle, 0);

        float turnTime = 0f;
        while (turnTime < 1f)
        {
            turnTime += Time.deltaTime * turnSpeed;
            key.transform.rotation = Quaternion.Slerp(originalRotation, finalRotation, turnTime);
            yield return null;
        }

        // 🔊 Sonido de rotura
        if (breakSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(breakSound);
        }

        // 🎮 Vibración del mando
        if (grab != null && grab.interactorsSelecting.Count > 0)
        {
            var interactor = grab.interactorsSelecting[0] as UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInputInteractor;
            if (interactor != null)
            {
                interactor.xrController.SendHapticImpulse(0.5f, 0.2f);
            }
        }

        yield return new WaitForSeconds(delayBeforeLock);

        // 🔒 Fijar la llave en su sitio
        Rigidbody rb = key.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        // ✅ Activar el XR Simple Interactable para detectar intentos frustrados
        XRSimpleInteractable simple = key.GetComponent<XRSimpleInteractable>();
        if (simple != null)
        {
            simple.enabled = true;

            // Buscar el collider del hijo
            Transform childTrigger = key.transform.Find("StuckTouchTrigger");
            if (childTrigger != null)
            {
                Collider triggerCollider = childTrigger.GetComponent<Collider>();
                if (triggerCollider != null)
                {
                    simple.colliders.Clear();
                    simple.colliders.Add(triggerCollider);
                }
            }
        }
    }
}
