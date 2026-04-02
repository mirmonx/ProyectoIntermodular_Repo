using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Collections;

public class PantallaNombre : MonoBehaviour
{
    // input donde el usuario escribe el nombre
    public TMP_InputField inputNombre;

    // texto que muestro por pantalla
    public TextMeshProUGUI textoTitulo;

    // variable para usar el nombre en otras escenas
    public static string nombreMascota;

    // URL del backend
    string url = "http://localhost:8080/guardarMascota";

    public void GuardarNombre()
    {
        // guardo el nombre
        nombreMascota = inputNombre.text;

        // lo guardo en el script empty para luego tener info unificada
        DatosMascota.nombre = inputNombre.text;

        // mensaje en pantalla
        textoTitulo.text = "Hola!!! Encantado/a de conocerte " + nombreMascota;

        Debug.Log("Nombre introducido: " + nombreMascota);

        // envio y cambio de escena 
        StartCoroutine(EnviarYcambiar());
    }

    //primero envia, luego cambia escena
    IEnumerator EnviarYcambiar()
    {
        yield return StartCoroutine(EnviarNombre());
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("GatoOperro");
    }

    // corrutina para enviar datos al backend unificados
    IEnumerator EnviarNombre()
    {
        WWWForm form = new WWWForm();//creo el formulario 

        form.AddField("nombre", DatosMascota.nombre);
        form.AddField("animal", DatosMascota.animal);
        form.AddField("detalle", DatosMascota.detalle);

        UnityWebRequest request = UnityWebRequest.Post(url, form);//le digo la url y el formulario

        yield return request.SendWebRequest();//espera a que el servidor responda sin congelar el juego

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Respuesta servidor: " + request.downloadHandler.text);
        }
        else//por si no funciona
        {
            Debug.Log("Error: " + request.error);
        }
    }
}