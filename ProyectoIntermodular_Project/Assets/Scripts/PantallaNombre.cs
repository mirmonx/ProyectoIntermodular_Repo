using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Collections;

public class PantallaNombre : MonoBehaviour
{
    // es donde el usuario escribe el nombre
    public TMP_InputField inputNombre;

    // texto que muestro por  pantalla
    public TextMeshProUGUI textoTitulo;

    // variable estática para guardar el nombre y usarlo en otras escenas
    public static string nombreMascota;

<<<<<<< Updated upstream
    // URL de ruta que he creado en js
    string url = "http://localhost:8080/guardarMascota";


=======
    //url del backend para guardarlo
    string url = "http://localhost:8080/guardarMascota";

>>>>>>> Stashed changes
    public void GuardarNombre()
    {
        // guardo el nombre 
        nombreMascota = inputNombre.text;

<<<<<<< Updated upstream
        // mensaje por pantalla
=======
        DatosMascota.nombre = inputNombre.text;//para guardarloi en el empty script y que nos muestre datos generales

>>>>>>> Stashed changes
        textoTitulo.text = "Hola!!! Encantado/a de conocerte " + nombreMascota;

        // se ve el nombre en la consola de unity
        Debug.Log("Nombre introducido: " + nombreMascota);

<<<<<<< Updated upstream
        // envio el nombre al backend
        StartCoroutine(EnviarNombre());

        // cambio de escena
        StartCoroutine(CambiarEscena());
    }


    // corrutina para enviar datos al servidor
    IEnumerator EnviarNombre()
=======
        //envio al backend
        StartCoroutine(EnviarNombre());

        // cambio de escena 
        StartCoroutine(CambiarEscena()); //funcion para ejecutar y pausar el juego
    }

    // corrutina para enviar datos al servidor
    IEnumerator EnviarNombre()
    {
        WWWForm form = new WWWForm();

        // envio los datos de la mascota
        form.AddField("nombre", DatosMascota.nombre);
        form.AddField("animal", DatosMascota.animal);
        form.AddField("detalle", DatosMascota.detalle);

        // creo la peticion POST
        UnityWebRequest request = UnityWebRequest.Post(url, form);

        // envío la petición y espero respuesta
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

    IEnumerator CambiarEscena() //he puesto IENUMERATOR porque es una funcion que se ejecuta por partes 
>>>>>>> Stashed changes
    {
        // formulario para enviar datos (POST)
        WWWForm form = new WWWForm();

        // campo "nombre" con el valor introducido por usuario
        form.AddField("nombre", nombreMascota);

        // Creo la petición POST al backend
        UnityWebRequest request = UnityWebRequest.Post(url, form);

        // envio petición y espero respuesta (asincronia)
        yield return request.SendWebRequest();

        // bucle para si ha ido bien muestre la respues en la consola del servidor
        if (request.result == UnityWebRequest.Result.Success)
        {
            
            Debug.Log("Respuesta servidor: " + request.downloadHandler.text);
        }
        else
        {
            // por si hay error
            Debug.Log("Error: " + request.error);
        }
    }


    //corrutina para cambiar de escena
    IEnumerator CambiarEscena()
    {
        //para esperar 3 segundos
        yield return new WaitForSeconds(3f);

        // cargo la escena siguiente
        SceneManager.LoadScene("GatoOperro");
    }
}