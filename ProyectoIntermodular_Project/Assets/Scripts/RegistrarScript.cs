using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System.Collections;
using System.Text;
using UnityEngine.SceneManagement;

[System.Serializable]
public class RespuestaRegistro
{
    public string mensaje; // Coincide con lo que envía el Backend
    public int id;         // Coincide con result.insertId
}

[System.Serializable]
public class UsuarioParaRegistro
{
    public string nombre;
    public string email;
    public string password;
}

public class RegistrarScript : MonoBehaviour
{
    public TMP_InputField inputNombre;
    public TMP_InputField inputEmail;
    public TMP_InputField inputPassword;

    string url = "http://localhost:8080/registro";

    public void RegistrarUsuario()
    {
        StartCoroutine(EnviarRegistro());
    }

    IEnumerator EnviarRegistro()
    {
        if (string.IsNullOrEmpty(inputNombre.text) ||
            string.IsNullOrEmpty(inputEmail.text) ||
            string.IsNullOrEmpty(inputPassword.text))
        {
            Debug.LogError("Faltan campos por rellenar");
            yield break;
        }

        // Usamos la clase serializable
        UsuarioParaRegistro datos = new UsuarioParaRegistro();
        datos.nombre = inputNombre.text;
        datos.email = inputEmail.text;
        datos.password = inputPassword.text;

        string json = JsonUtility.ToJson(datos);
        Debug.Log("Enviando JSON: " + json);

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);

        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Servidor dice: " + request.downloadHandler.text);
            RespuestaRegistro respuesta = JsonUtility.FromJson<RespuestaRegistro>(request.downloadHandler.text);

            Debug.Log("Usuario registrado con ID: " + respuesta.id);

            // Guardamos el ID para usarlo luego
            PlayerPrefs.SetInt("usuario_id", respuesta.id);
            PlayerPrefs.Save();

            // Como es un registro nuevo, SEGURO que no tiene mascota
            SceneManager.LoadScene("NombreMascota");
        }
        else
        {
            Debug.LogError("Error en registro: " + request.error);
            Debug.LogError("Detalle del servidor: " + request.downloadHandler.text);
        }
    }
}
