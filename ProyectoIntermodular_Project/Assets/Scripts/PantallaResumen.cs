using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text;
using UnityEngine.SceneManagement;

public class PantallaResumen : MonoBehaviour
{
    //NGROK
    string url = "https://rippling-sinless-margarita.ngrok-free.dev/guardarMascota";

    public void GuardarMascotaFinal()
    {
        StartCoroutine(EnviarMascota());
    }

    IEnumerator EnviarMascota()
    {
        int usuario_id = PlayerPrefs.GetInt("usuario_id");

        Debug.Log("Nombre: " + DatosMascota.nombre);
        Debug.Log("Tipo: " + DatosMascota.tipo);
        Debug.Log("Pelaje: " + DatosMascota.pelaje);
        Debug.Log("Tamaño: " + DatosMascota.tamano);
        Debug.Log("Usuario ID: " + usuario_id);

        var datos = new
        {
            nombre = DatosMascota.nombre,
            tipo = DatosMascota.tipo,
            pelaje = DatosMascota.pelaje,
            tamano = DatosMascota.tamano,
            edad = DatosMascota.edad,
            usuario_id = usuario_id
        };

        string json = JsonUtility.ToJson(datos);

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);

        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Mascota guardada correctamente");
            SceneManager.LoadScene("Home");
        }
        else
        {
            Debug.LogError("Error backend: " + request.downloadHandler.text);
        }
    }
}