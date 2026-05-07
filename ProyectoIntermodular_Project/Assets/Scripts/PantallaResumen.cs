using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text;
using UnityEngine.SceneManagement;

public class PantallaResumen : MonoBehaviour
{
    string url = "http://localhost:8080/guardar-mascota";

    public void GuardarMascotaFinal()
    {
        StartCoroutine(EnviarMascota());
    }

    IEnumerator EnviarMascota()
    {
        int usuario_id = PlayerPrefs.GetInt("id_usuario");

        // DEBUG
        Debug.Log("Nombre: " + DatosMascota.nombre);
        Debug.Log("Tipo: " + DatosMascota.tipo);
        Debug.Log("Pelaje: " + DatosMascota.pelaje);
        Debug.Log("Tamaño: " + DatosMascota.size);
        Debug.Log("Usuario ID: " + usuario_id);

        MascotaFinalJSON datosEnvio = new MascotaFinalJSON();
        datosEnvio.nombre = DatosMascota.nombre;
        datosEnvio.tipo_mascota = DatosMascota.tipo;
        datosEnvio.pelaje = DatosMascota.pelaje;
        datosEnvio.size = DatosMascota.size;
        datosEnvio.edad = DatosMascota.edad;
        datosEnvio.usuario_id = usuario_id;
        datosEnvio.foto_url = DatosMascota.foto_url; // para cloudinary

        string json = JsonUtility.ToJson(datosEnvio);

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

[System.Serializable]
public class MascotaFinalJSON
{
    public string nombre;
    public string tipo_mascota;
    public string pelaje;
    public string size;
    public string edad;
    public int usuario_id;
    public string foto_url; //para añadir también la foto de cloudinary
}