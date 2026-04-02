using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PantallaPerroOGato : MonoBehaviour
{
    public TMP_Text textoSaludo;

    // URL del backend
    string url = "http://localhost:8080/seleccionAnimal";

    void Start()
    {
        // muestro el nombre de la pantalla anterior
        textoSaludo.text = "Hola " + PantallaNombre.nombreMascota;
    }

    public void SeleccionarGato()
    {
        Debug.Log(PantallaNombre.nombreMascota + " es un gato");

        // guardo en script global
        DatosMascota.animal = "gato";

        // envío al backend
        StartCoroutine(EnviarAnimal("gato"));
    }

    public void SeleccionarPerro()
    {
        Debug.Log(PantallaNombre.nombreMascota + " es un perro");

        // guardo en script global
        DatosMascota.animal = "perro";

        // envio al backend
        StartCoroutine(EnviarAnimal("perro"));
    }

    // corrutina para enviar datos al servidor
    IEnumerator EnviarAnimal(string animal)
    {
        WWWForm form = new WWWForm();

        // envio el tipo de animal seleccionado
        form.AddField("animal", animal);

        // creo la peticion POST
        UnityWebRequest request = UnityWebRequest.Post(url, form);

        // envio y espero respuesta
        yield return request.SendWebRequest();

        // si todo ha ido bien
        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Respuesta servidor: " + request.downloadHandler.text);
        }
        else//por si no ha ido bien que muestre el error
        {
            Debug.Log("Error: " + request.error);
        }
    }
}