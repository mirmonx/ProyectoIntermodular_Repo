using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PantallaGato : MonoBehaviour
{
    public TMP_Text textoPelo;

    // URL del backend para guardar
    string url = "http://localhost:8080/tipoGato";

    public void PeloCalvo()
    {
        textoPelo.text = "Tu gato es calvo.";

        //envio al backend
        StartCoroutine(EnviarTipo("calvo"));

        DatosMascota.detalle = "calvo";
    }

    public void PeloCorto()
    {
        textoPelo.text = "Tu gato tiene el pelo corto.";

        //envio al backend
        StartCoroutine(EnviarTipo("corto"));

        DatosMascota.detalle = "corto";
    }

    public void PeloLargo()
    {
        textoPelo.text = "Tu gato tiene el pelo largo.";

        // envio al backend
        StartCoroutine(EnviarTipo("largo"));

        DatosMascota.detalle = "largo";
    }

    // corrutina para enviar datos al servidor
    IEnumerator EnviarTipo(string tipo)
    {
        WWWForm form = new WWWForm();

        // envio el tipo de pelaje del gato
        form.AddField("tipo", tipo);

        // creo la petición POST
        UnityWebRequest request = UnityWebRequest.Post(url, form);

        // envio la peticion y espero respuesta
        yield return request.SendWebRequest();

        // si todo ha ido bien
        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Respuesta servidor: " + request.downloadHandler.text);
        }
        else
        {
            Debug.Log("Error: " + request.error);
        }
    }
}