using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PantallaPerroOGato : MonoBehaviour
{
    public TMP_Text textoSaludo;

    string url = "http://localhost:8080/seleccionAnimal";

    void Start()
    {
        textoSaludo.text = "Hola " + PantallaNombre.nombreMascota;
    }

    public void SeleccionarGato()
    {
        Debug.Log(PantallaNombre.nombreMascota + " es un gato");
        StartCoroutine(EnviarAnimal("gato"));
    }

    public void SeleccionarPerro()
    {
        Debug.Log(PantallaNombre.nombreMascota + " es un perro");
        StartCoroutine(EnviarAnimal("perro"));
    }

    IEnumerator EnviarAnimal(string animal)
    {
        WWWForm form = new WWWForm();
        form.AddField("animal", animal);

        UnityWebRequest request = UnityWebRequest.Post(url, form);

        yield return request.SendWebRequest();

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