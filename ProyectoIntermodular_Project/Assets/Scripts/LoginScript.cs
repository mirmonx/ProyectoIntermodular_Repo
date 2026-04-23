using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System.Collections;
using System.Text;
using UnityEngine.SceneManagement;


// clase para leer respuesta del backend
[System.Serializable]
public class RespuestaLogin
{
    public int id;
    public string nombre;
    public string email;
    public bool tieneMascota;
}

public class LoginScript : MonoBehaviour
{
    public TMP_InputField inputEmail;
    public TMP_InputField inputPassword;
    public TMP_Text textoError;

    string url = "http://localhost:8080/login";

    public void IniciarSesion()
    {
        StartCoroutine(Login());
    }

    IEnumerator Login()
    {     

        // validar campos
        if (string.IsNullOrEmpty(inputEmail.text) || string.IsNullOrEmpty(inputPassword.text))
        {
            textoError.text = "Rellena todos los campos";
            yield break;
        }

        // crear JSON
        Usuario datos = new Usuario();
        datos.email = inputEmail.text;
        datos.password = inputPassword.text;

        string json = JsonUtility.ToJson(datos);

        // crear request
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        Debug.Log("Enviando datos al servidor: " + json); // <--- DEBUG PARA VER SI LLEGA AQUÍ

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            // convertir respuesta
            RespuestaLogin usuario =
                JsonUtility.FromJson<RespuestaLogin>(request.downloadHandler.text);

            Debug.Log("Login correcto. Usuario ID: " + usuario.id);

            // guardar usuario logueado
            PlayerPrefs.SetInt("usuario_id", usuario.id);
            PlayerPrefs.Save(); 

            
            if (usuario.tieneMascota)
            {
                SceneManager.LoadScene("Perfil"); // ya tiene mascota
            }
            else
            {
                SceneManager.LoadScene("NombreMascota"); // primera vez
            }
        }
        else
        {
            Debug.LogError("Error login: " + request.error);
            textoError.text = "Email o contraseña incorrectos";
        }
    }
}