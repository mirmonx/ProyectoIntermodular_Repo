using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Collections;
using System.Text;

public class PantallaGato : MonoBehaviour
{
    public TMP_Text textoPelo;
    string url = "http://localhost:8080/guardar-mascota";

    // Llamaremos a esta función desde los botones pasando "calvo", "corto" o "largo"
    public void SeleccionarPelaje(string tipoPelo)
    {
        DatosMascota.pelaje = tipoPelo;
        DatosMascota.size = "N/A"; // Los gatos no tienen tamaño en tu lógica

        if (textoPelo != null)
            textoPelo.text = "Tu gato tiene el pelo " + tipoPelo;

        Debug.Log("Guardando gato: " + DatosMascota.nombre + " con pelo " + tipoPelo);

        // Iniciamos el envío al servidor
        StartCoroutine(EnviarMascota());
    }

    IEnumerator EnviarMascota()
    {
        // Creamos el objeto para el JSON
        MascotaGatoJSON info = new MascotaGatoJSON();
        info.nombre = DatosMascota.nombre;
        info.tipo_mascota = DatosMascota.tipo;
        info.pelaje = DatosMascota.pelaje;
        info.size = DatosMascota.size;
        info.usuario_id = PlayerPrefs.GetInt("id_usuario");

        string json = JsonUtility.ToJson(info);

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Mascota guardada en la DB");
            // ✅ Ahora sí, cuando el server confirme, vamos al Perfil
            SceneManager.LoadScene("Perfil");
        }
        else
        {
            Debug.LogError("Error al guardar mascota: " + request.error);
        }
    }
}

// Clase auxiliar para el JSON (puedes ponerla al final del archivo)
[System.Serializable]
public class MascotaGatoJSON
{
    public string nombre;
    public string tipo_mascota;
    public string pelaje;
    public string size;
    public int usuario_id;
}