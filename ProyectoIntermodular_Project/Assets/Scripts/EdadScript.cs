using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System.Collections;
using System.Text;
using UnityEngine.SceneManagement;

public class PantallaEdad : MonoBehaviour
{
    public TMP_InputField inputEdad;
    string urlUpdate = "http://localhost:8080/actualizar-edad";

    public void GuardarEdad()
    {
        // 1. Validamos que no esté vacío
        if (string.IsNullOrEmpty(inputEdad.text))
        {
            Debug.LogWarning("Por favor, escribe la edad antes de continuar.");
            return;
        }

        // 2. Guardamos en la clase estática
        DatosMascota.edad = inputEdad.text;

        // 3. Enviamos al servidor
        StartCoroutine(EnviarEdadAlServer(inputEdad.text));
    }

    IEnumerator EnviarEdadAlServer(string edadTexto)
    {
        int mascotaId = PlayerPrefs.GetInt("mascota_id");

        // Creamos un objeto para serializarlo bien como JSON
        ActualizarEdadDTO datos = new ActualizarEdadDTO();
        datos.id = mascotaId;
        datos.edad = edadTexto;

        string json = JsonUtility.ToJson(datos);

        UnityWebRequest request = new UnityWebRequest(urlUpdate, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            SceneManager.LoadScene("Petfil");
        }
        else
        {
            Debug.LogError("Error: " + request.error);
        }
    }
}

[System.Serializable]
public class ActualizarEdadDTO
{
    public int id;
    public string edad;
}