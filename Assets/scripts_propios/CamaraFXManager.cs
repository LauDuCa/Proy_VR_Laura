using UnityEngine;

public class CamaraFXManager : MonoBehaviour
{
    private Camera cam;
    private Vector3 initialRotation;
    private Vector3 initialPosition;

    private bool zoomActivo = false;
    private float objetivoFOV;
    private float velocidadZoom;

    private bool tiltActivo = false;
    private float amplitudTilt;
    private float tiempoTilt = 0f;

    private bool flickerActivo = false;
    private float velocidadFlicker;
    public CanvasGroup flickerOverlay;

    private bool vibrar = false;
    private float intensidadVibracion;
    private float duracionVibracion;
    private float tiempoVibracion = 0f;

    void Start()
    {
        cam = GetComponent<Camera>();
        initialRotation = transform.localEulerAngles;
        initialPosition = transform.localPosition;

        if (flickerOverlay != null)
            flickerOverlay.alpha = 0;
    }

    void Update()
    {
        if (tiltActivo)
        {
            tiempoTilt += Time.deltaTime;
            float tiltZ = Mathf.Sin(tiempoTilt * 2f) * amplitudTilt;
            transform.localEulerAngles = new Vector3(initialRotation.x, initialRotation.y, tiltZ);
        }

        if (zoomActivo && cam != null)
        {
            cam.fieldOfView = Mathf.MoveTowards(cam.fieldOfView, objetivoFOV, velocidadZoom * Time.deltaTime);
        }

        if (flickerActivo && flickerOverlay != null)
        {
            float alpha = Mathf.Abs(Mathf.Sin(Time.time * velocidadFlicker));
            flickerOverlay.alpha = alpha;
        }

        if (vibrar)
        {
            tiempoVibracion += Time.deltaTime;
            transform.localPosition = initialPosition + Random.insideUnitSphere * intensidadVibracion;

            if (tiempoVibracion >= duracionVibracion)
            {
                vibrar = false;
                tiempoVibracion = 0f;
                transform.localPosition = initialPosition;
            }
        }
    }

    public void ActivarTilt(float amplitud)
    {
        amplitudTilt = amplitud;
        tiltActivo = true;
        tiempoTilt = 0f;
    }

    public void ActivarZoom(float fovObjetivo, float velocidad)
    {
        objetivoFOV = fovObjetivo;
        velocidadZoom = velocidad;
        zoomActivo = true;
    }

    public void ActivarFlicker(float velocidad)
    {
        velocidadFlicker = velocidad;
        flickerActivo = true;
    }

    public void DesactivarFlicker()
    {
        flickerActivo = false;
        if (flickerOverlay != null)
            flickerOverlay.alpha = 0;
    }

    public void ActivarVibracionCorta(float intensidad, float duracion)
    {
        intensidadVibracion = intensidad;
        duracionVibracion = duracion;
        vibrar = true;
        tiempoVibracion = 0f;
    }
}
