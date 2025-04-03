using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AlarmaProgresiva : MonoBehaviour
{
    public float volumenInicial = 0.1f;
    public float volumenFinal = 1f;
    public float duracion = 30f;

    private AudioSource audioSource;
    private float tiempoTranscurrido = 0f;
    private bool activado = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = volumenInicial;
        ActivarAlarma(); // Se activa sola al comenzar
    }

    void Update()
    {
        if (activado && tiempoTranscurrido < duracion)
        {
            tiempoTranscurrido += Time.deltaTime;
            float t = Mathf.Clamp01(tiempoTranscurrido / duracion);
            audioSource.volume = Mathf.Lerp(volumenInicial, volumenFinal, t);
        }
    }

    public void ActivarAlarma()
    {
        if (!activado)
        {
            activado = true;
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
    }
}