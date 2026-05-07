using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Collections;
using System.Text;

public class PantallaPerro : MonoBehaviour
{
    public TMP_Text textoRaza;
    string url = "http://localhost:8080/guardar-mascota";

    // Función unificada para los botones: pequeño, mediano, grande
    public void SeleccionarTamano(string tamanoSeleccionado)
    {
        DatosMascota.size = tamanoSeleccionado;
        DatosMascota.pelaje = "N/A"; // Los perros no eligen pelaje en tu flujo

        if (textoRaza != null)
            textoRaza.text = "Tu perro es " + tamanoSeleccionado;

        Debug.Log("Guardando perro: " + DatosMascota.nombre + " de tamaño " + tamanoSeleccionado);

        // Lanzamos el guardado
        StartCoroutine(EnviarMascota());
    }

    IEnumerator EnviarMascota()
    {
        // Usamos la misma estructura que con el gato
        MascotaPerroJSON info = new MascotaPerroJSON();
        info.nombre = DatosMascota.nombre;
        info.tipo_mascota = DatosMascota.tipo; // Corregido: tipo_mascota coincide con el backend
        info.pelaje = DatosMascota.pelaje;
        info.size = DatosMascota.size;
        info.usuario_id = PlayerPrefs.GetInt("id_usuario");
        info.foto_url = DatosMascota.foto_url;

        string json = JsonUtility.ToJson(info);

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();

        // Añadimos la cabecera para que el servidor reconozca el JSON
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("¡Perro guardado con éxito!");
            SceneManager.LoadScene("Perfil");
        }
        else
        {
            // Mostramos el error detallado del servidor para depurar
            Debug.LogError("Error al guardar perro: " + request.error);
            Debug.LogError("Respuesta del servidor: " + request.downloadHandler.text);
        }
    }
}

// Clase  para el JSON
[System.Serializable]
public class MascotaPerroJSON
{
    public string nombre;
    public string tipo_mascota;
    public string pelaje;
    public string size;
    public int usuario_id;
    public string foto_url;
}