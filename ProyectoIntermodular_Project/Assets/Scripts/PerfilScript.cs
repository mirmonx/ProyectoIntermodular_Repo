using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

[System.Serializable]
public class DatosPerfil
{
    public string usuario_nombre;
    public string mascota_nombre;
    public int mascota_id;
    public string edad;
    public string foto_url;
    public string foto_perfil;
}

public class PerfilScript : MonoBehaviour
{
    public TMP_Text textoUsuario;
    public TMP_Text textoMascota;

    // AÑADIR ESTO
    public RawImage imagenPerfil;
    public RawImage fotoMascota;

    string urlBase = "http://localhost:8080/perfil/";

    void Start()
    {
        int idGuardado = PlayerPrefs.GetInt("id_usuario");

        if (idGuardado != 0)
        {
            StartCoroutine(CargarPerfil(idGuardado));
        }
        else
        {
            Debug.LogError("No se encontró su id_usuario. ¿Has hecho login?");
        }
    }

    IEnumerator CargarPerfil(int id)
    {
        UnityWebRequest request = UnityWebRequest.Get(urlBase + id);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            DatosPerfil datos =
                JsonUtility.FromJson<DatosPerfil>(
                    request.downloadHandler.text

                );
            Debug.Log(request.downloadHandler.text);

            Debug.Log("URL FOTO PERFIL:");
            Debug.Log(datos.foto_perfil);

            DatosMascota.foto_url = datos.foto_url;

            // DESCARGAR FOTO
            if (!string.IsNullOrEmpty(datos.foto_url))
            {
                StartCoroutine(DescargarFoto(datos.foto_url));
            }
            if (!string.IsNullOrEmpty(datos.foto_perfil))
            {
                StartCoroutine(DescargarFotoPerfil(datos.foto_perfil));
            }

            if (textoUsuario != null)
                textoUsuario.text =
                    "¡Hola, " + datos.usuario_nombre + "!";

            if (textoMascota != null)
                textoMascota.text =
                    datos.mascota_nombre;

            DatosMascota.nombre = datos.mascota_nombre;

            DatosMascota.edad =
                string.IsNullOrEmpty(datos.edad)
                ? ""
                : datos.edad;

            PlayerPrefs.SetInt("mascota_id", datos.mascota_id);

            PlayerPrefs.Save();
        }
        else
        {
            Debug.LogError(
                "Error al cargar perfil: " + request.error
            );
        }
    }

    // NUEVA COROUTINE
    IEnumerator DescargarFoto(string url)
    {
        UnityWebRequest requestFoto =
            UnityWebRequestTexture.GetTexture(url);

        yield return requestFoto.SendWebRequest();

        if (requestFoto.result == UnityWebRequest.Result.Success)
        {
            Texture textura =
                DownloadHandlerTexture.GetContent(requestFoto);

            fotoMascota.texture = textura;
        }
        else
        {
            Debug.LogError(
                "Error cargando foto: " +
                requestFoto.error
            );
        }
    }

    IEnumerator DescargarFotoPerfil(string url)
    {
        UnityWebRequest requestFoto =
            UnityWebRequestTexture.GetTexture(url);

        yield return requestFoto.SendWebRequest();

        if (requestFoto.result == UnityWebRequest.Result.Success)
        {
            Texture textura =
                DownloadHandlerTexture.GetContent(requestFoto);

            imagenPerfil.texture = textura;
        }
        else
        {
            Debug.LogError(
                "Error cargando foto perfil: " +
                requestFoto.error
            );
        }
    }

    public void AbrirDetalleMascota()
    {
        string edadParaRevisar = "";

        if (DatosMascota.edad != null)
        {
            edadParaRevisar =
                DatosMascota.edad.Trim().ToLower();
        }

        if (string.IsNullOrEmpty(edadParaRevisar) ||
            edadParaRevisar == "null" ||
            edadParaRevisar == "" ||
            edadParaRevisar == "n/a")
        {
            SceneManager.LoadScene("EdadMascota");
        }
        else
        {
            SceneManager.LoadScene("Petfil");
        }
    }
}