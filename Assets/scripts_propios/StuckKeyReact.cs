using UnityEngine;
using System.Collections;

public class StuckKeyReact : MonoBehaviour
{
    public Transform visualTransform; // ¡Arrástrale Visual_Key aquí!

    public float rotationAmount = 5f;
    public float positionAmount = 0.01f;
    public float speed = 30f;
    public float duration = 0.3f;
    public AudioClip stuckSound;

    private AudioSource audioSource;
    private bool isShaking = false;

    private Quaternion originalRot;
    private Vector3 originalPos;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        originalRot = visualTransform.localRotation;
        originalPos = visualTransform.localPosition;
    }

    public void OnTouch()
    {
        if (!isShaking)
        {
            StartCoroutine(ShakeAndSound());
        }
    }

    private IEnumerator ShakeAndSound()
    {
        isShaking = true;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float angle = Mathf.Sin(time * speed) * rotationAmount;
            float offset = Mathf.Sin(time * speed * 2f) * positionAmount;

            visualTransform.localRotation = originalRot * Quaternion.Euler(0, angle, 0);
            visualTransform.localPosition = originalPos + visualTransform.right * offset;

            yield return null;
        }

        visualTransform.localRotation = originalRot;
        visualTransform.localPosition = originalPos;

        if (stuckSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(stuckSound);
        }

        isShaking = false;
    }
}