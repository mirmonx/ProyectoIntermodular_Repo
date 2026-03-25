using TMPro;
using UnityEngine;

public class PantallaGato : MonoBehaviour
{
    public TMP_Text textoPelo;

    public void PeloCorto()
    {
        textoPelo.text = "Tu gato tiene el pelo corto.";
    }

    public void PeloMediano()
    {
        textoPelo.text = "Tu gato tiene el pelo mediano.";
    }

    public void PeloLargo()
    {
        textoPelo.text = "Tu gato tiene el pelo largo.";
    }
}
