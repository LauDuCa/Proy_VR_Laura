using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalCaotico : MonoBehaviour
{
    public AudioSource sonidoFinal;
    public AudioClip golpeSeco;

    public Camera camara;
    public CanvasGroup pantallaNegra;
    public Light luzCaos;
    public float intensidadMaxima = 10f;

    private float tiempo = 0f;
    private bool activado = false;

    void Start()
    {
        if (pantallaNegra != null)
            pantallaNegra.alpha = 0;

        if (luzCaos != null)
        {
            luzCaos.enabled = false;
            luzCaos.intensity = intensidadMaxima;
        }
    }

    public void ActivarFinal()
    {
        if (activado) return;
        activado = true;

        if (luzCaos != null)
            luzCaos.enabled = true;

        if (sonidoFinal != null && golpeSeco != null)
            sonidoFinal.PlayOneShot(golpeSeco);

        Invoke("PantallaNegra", 0.8f);
        Invoke("TerminarJuego", 2.5f);
    }

    void Update()
    {
        if (!activado || camara == null) return;

        tiempo += Time.deltaTime * 60f;
        float vibracion = Mathf.Sin(tiempo) * 0.1f;
        camara.transform.localPosition = Random.insideUnitSphere * vibracion;
    }

    void PantallaNegra()
    {
        if (pantallaNegra != null)
            pantallaNegra.alpha = 1;
    }

    void TerminarJuego()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}