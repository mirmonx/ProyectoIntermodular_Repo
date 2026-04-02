using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PantallaPerro : MonoBehaviour
{
    public TMP_Text textoRaza;

    // URL del backend para guardarlo
    string url = "http://localhost:8080/tipoPerro";

    public void PerroPequeño()
    {
        textoRaza.text = "Tu perro es de raza pequeña.";

        //envio al backend
        StartCoroutine(EnviarTamano("pequeño"));

        DatosMascota.detalle = "pequeño";//para que se guarde en el empty script y muestre datos completos al final
    }

    public void PerroMediano()
    {
        textoRaza.text = "Tu perro es de raza mediana.";

        //envio al backend
        StartCoroutine(EnviarTamano("mediano"));

        DatosMascota.detalle = "mediano";//para que se guarde en el empty script y muestre datos completos al final
    }

    public void PerroGrande()
    {
        textoRaza.text = "Tu perro es de raza grande.";

        //envio al backend
        StartCoroutine(EnviarTamano("grande"));

        DatosMascota.detalle = "grande";//para que se guarde en el empty script y muestre datos completos al final
    }

    // corrutina para enviar datos al servidor
    IEnumerator EnviarTamano(string tamano)
    {
        WWWForm form = new WWWForm();

        // envio el tamaño del perro seleccionado
        form.AddField("tamano", tamano);

        //creo la peticion POST
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