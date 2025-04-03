using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    public GameObject Trigger_Armario;
    public GameObject Trigger_Llave;
    public GameObject Trigger_Carpeta;
    public GameObject Trigger_Maquina;

    public void ActivarTriggerArmario()
    {
        if (Trigger_Armario != null)
            Trigger_Armario.SetActive(true);
    }

    public void ActivarTriggerLlave()
    {
        if (Trigger_Llave != null)
            Trigger_Llave.SetActive(true);
    }

    public void ActivarTriggerCarpeta()
    {
        if (Trigger_Carpeta != null)
            Trigger_Carpeta.SetActive(true);
    }

    public void ActivarTriggerMaquina()
    {
        if (Trigger_Maquina != null)
            Trigger_Maquina.SetActive(true);
    }
}