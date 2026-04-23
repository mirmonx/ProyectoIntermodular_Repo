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
        DatosMascota.tamano = tamanoSeleccionado;
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
        MascotaJSON info = new MascotaJSON();
        info.nombre = DatosMascota.nombre;
        info.tipo_mascota = DatosMascota.tipo;
        info.pelaje = DatosMascota.pelaje;
        info.tamano = DatosMascota.tamano;
        info.usuario_id = PlayerPrefs.GetInt("usuario_id");

        string json = JsonUtility.ToJson(info);

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("¡Perro guardado con éxito!");
            SceneManager.LoadScene("Perfil");
        }
        else
        {
            Debug.LogError("Error al guardar perro: " + request.error);
        }
    }
}
