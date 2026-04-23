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

    //NGROK
    string url = "https://rippling-sinless-margarita.ngrok-free.dev/login";

    public void IniciarSesion()
    {
        StartCoroutine(Login());
    }

    IEnumerator Login()
    {
        if (string.IsNullOrEmpty(inputEmail.text) || string.IsNullOrEmpty(inputPassword.text))
        {
            textoError.text = "Rellena todos los campos";
            yield break;
        }

        Usuario datos = new Usuario();
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
              
                Debug.Log("RESPUESTA RAW: " + request.downloadHandler.text);

                RespuestaLogin usuario =
                    JsonUtility.FromJson<RespuestaLogin>(request.downloadHandler.text);

                Debug.Log("ID RECIBIDO: " + usuario.id);

                PlayerPrefs.SetInt("usuario_id", usuario.id);
                PlayerPrefs.Save();

                Debug.Log("ID GUARDADO: " + PlayerPrefs.GetInt("usuario_id"));

                if (usuario.tieneMascota)
                {
                    SceneManager.LoadScene("Home");
                }
                else
                {
                    SceneManager.LoadScene("NombreMascota");
                }
            }
            else
        {
            Debug.LogError("Error login: " + request.downloadHandler.text);
            textoError.text = "Email o contraseña incorrectos";
        }
    }
}