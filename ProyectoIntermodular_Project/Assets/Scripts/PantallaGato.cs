using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PantallaGato : MonoBehaviour
{
    public TMP_Text textoPelo;

    public void PeloCalvo()
    {
        textoPelo.text = "Tu gato es calvo.";
        DatosMascota.pelaje = "calvo";
        SceneManager.LoadScene("Petfil"); // ✅ escena real
    }

    public void PeloCorto()
    {
        textoPelo.text = "Tu gato tiene el pelo corto.";
        DatosMascota.pelaje = "corto";
        SceneManager.LoadScene("Petfil");
    }

    public void PeloLargo()
    {
        textoPelo.text = "Tu gato tiene el pelo largo.";
        DatosMascota.pelaje = "largo";
        SceneManager.LoadScene("Petfil");
    }
}