using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System.Collections;
using System.Text;
using UnityEngine.SceneManagement;

[System.Serializable]
public class RespuestaRegistro
{
    public string msg;
    public int usuario_id;
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
            Debug.LogError("Faltan campos");
            yield break;
        }

        Usuario datos = new Usuario();
        datos.nombre = inputNombre.text;
        datos.email = inputEmail.text;
        datos.password = inputPassword.text;

        string json = JsonUtility.ToJson(datos);

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);

        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            RespuestaRegistro respuesta = JsonUtility.FromJson<RespuestaRegistro>(request.downloadHandler.text);

            Debug.Log("Usuario registrado ID: " + respuesta.usuario_id);

            // guardamos usuario
            PlayerPrefs.SetInt("usuario_id", respuesta.usuario_id);

            // cambiar escena
            SceneManager.LoadScene("NombreMascota");
        }
        else
        {
            Debug.LogError("Error: " + request.error);
            Debug.LogError("Detalle: " + request.downloadHandler.text);
        }
    }
}