using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System.Collections;
using System.Text;
using UnityEngine.SceneManagement;

public class RecuperarPasswordScript : MonoBehaviour
{
    public TMP_InputField inputEmail;
    public TMP_InputField inputNuevaPassword;
    public TMP_InputField inputConfirmarPassword;
    public TMP_Text textoMensaje;

    string url = "http://localhost:8080/recuperarPassword";

    public void GuardarNuevaPassword()
    {
        StartCoroutine(EnviarNuevaPassword());
    }

    IEnumerator EnviarNuevaPassword()
    {
        textoMensaje.text = "";

        // validar campos
        if (string.IsNullOrEmpty(inputEmail.text) ||
            string.IsNullOrEmpty(inputNuevaPassword.text) ||
            string.IsNullOrEmpty(inputConfirmarPassword.text))
        {
            textoMensaje.text = "Rellena todos los campos";
            yield break;
        }

        if (inputNuevaPassword.text != inputConfirmarPassword.text)
        {
            textoMensaje.text = "Las contraseñas no coinciden";
            yield break;
        }

        if (inputNuevaPassword.text.Length < 4)
        {
            textoMensaje.text = "Mínimo 4 caracteres";
            yield break;
        }

        // crear JSON
        var datos = new
        {
            email = inputEmail.text,
            nuevaPassword = inputNuevaPassword.text
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
            Debug.Log("Contraseña actualizada");

            textoMensaje.text = "Contraseña actualizada";

            yield return new WaitForSeconds(2f);

            SceneManager.LoadScene("Login");
        }
        else
        {
            Debug.LogError("Error: " + request.downloadHandler.text);
            textoMensaje.text = "Error o email no encontrado";
        }
    }
}