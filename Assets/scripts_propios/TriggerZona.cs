using UnityEngine;

public class TriggerZona : MonoBehaviour
{
    [Header("Objetos a activar")]
    public GameObject luzAEncender;
    public GameObject objetoInteractivo;
    public AudioSource sonidoZona;

    [Header("Efectos de cámara")]
    public bool activarTilt = false;
    public float tiltAmplitude = 3f;

    public bool activarZoom = false;
    public float targetFOV = 40f;
    public float zoomSpeed = 5f;

    public bool activarFlicker = false;
    public float flickerSpeed = 10f;

    public bool activarVibracion = false;
    public float vibracionIntensidad = 0.01f;
    public float vibracionDuracion = 0.5f;

    [Header("Avance de fase")]
    public bool activarSiguienteFase = false;
    public string metodoSiguienteFase = ""; // Ej: "ActivarTriggerLlave"

    private bool activado = false;

    void OnTriggerEnter(Collider other)
    {
        if (activado) return;
        if (other.CompareTag("Player"))
        {
            activado = true;

            if (luzAEncender != null) luzAEncender.SetActive(true);
            if (objetoInteractivo != null) objetoInteractivo.SetActive(true);
            if (sonidoZona != null) sonidoZona.Play();

            CamaraFXManager fx = FindObjectOfType<CamaraFXManager>();
            if (fx != null)
            {
                if (activarTilt) fx.ActivarTilt(tiltAmplitude);
                if (activarZoom) fx.ActivarZoom(targetFOV, zoomSpeed);
                if (activarFlicker) fx.ActivarFlicker(flickerSpeed);
                if (activarVibracion) fx.ActivarVibracionCorta(vibracionIntensidad, vibracionDuracion);
            }

            if (activarSiguienteFase && !string.IsNullOrEmpty(metodoSiguienteFase))
            {
                TriggerManager manager = FindObjectOfType<TriggerManager>();
                if (manager != null)
                    manager.Invoke(metodoSiguienteFase, 0f);
            }
        }
    }
}