using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PantallaPerroOGato : MonoBehaviour
{
    public TMP_Text textoSaludo;

<<<<<<< Updated upstream
=======
    // URL del backend
>>>>>>> Stashed changes
    string url = "http://localhost:8080/seleccionAnimal";

    void Start()
    {
        textoSaludo.text = "Hola " + PantallaNombre.nombreMascota;
<<<<<<< Updated upstream
=======
        // muestra el nombre de la anterior pantalla
>>>>>>> Stashed changes
    }

    public void SeleccionarGato()
    {
        Debug.Log(PantallaNombre.nombreMascota + " es un gato");
<<<<<<< Updated upstream
        StartCoroutine(EnviarAnimal("gato"));
    }
=======

        //envio al backend que es gato
        StartCoroutine(EnviarAnimal("gato"));

        DatosMascota.animal = "gato";// para que se guarde en el empty scrit y al final devuelva datos generales
    
}
>>>>>>> Stashed changes

    public void SeleccionarPerro()
    {
        Debug.Log(PantallaNombre.nombreMascota + " es un perro");
<<<<<<< Updated upstream
        StartCoroutine(EnviarAnimal("perro"));
    }

    IEnumerator EnviarAnimal(string animal)
    {
        WWWForm form = new WWWForm();
        form.AddField("animal", animal);

        UnityWebRequest request = UnityWebRequest.Post(url, form);

        yield return request.SendWebRequest();

=======

        //envío al backend que es perro
        StartCoroutine(EnviarAnimal("perro"));

        DatosMascota.animal = "perro";// para que se guarde en el empty scrit y al final devuelva datos generales
    }

    // corrutina para enviar datos al servidor
    IEnumerator EnviarAnimal(string animal)
    {
        WWWForm form = new WWWForm();

        // envío el tipo de animal seleccionado
        form.AddField("animal", animal);

        // creo la petición POST
        UnityWebRequest request = UnityWebRequest.Post(url, form);

        // envio y espero respuesta
        yield return request.SendWebRequest();

        // si todo ha ido bien
>>>>>>> Stashed changes
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