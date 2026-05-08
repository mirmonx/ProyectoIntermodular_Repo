using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Text;
using System.IO;

[System.Serializable]
public class DatosEditarPerfil
{
    public int id_usuario;

    public string nombre_usuario;
    public string foto_perfil;

    public string nombre_mascota;
    public string foto_mascota;
}

[System.Serializable]
public class DatosPerfilEditar
{
    public string usuario_nombre;
    public string mascota_nombre;
    public string foto_perfil;
    public string foto_url;
}

public class EditarPerfilScript : MonoBehaviour
{
    [Header("Inputs")]
    public TMP_InputField inputNombreUsuario;
    public TMP_InputField inputNombreMascota;

    [Header("UI")]
    public TMP_Text mensajeTexto;

    [Header("Script UI")]
    public UIEditarPerfil uiEditarPerfil;

    [Header("Imágenes")]
    public RawImage imagenPerfil;
    public RawImage imagenMascota;

    string urlEditar = "http://localhost:8080/editarPerfil";
    string urlSubir = "http://localhost:8080/subir-foto";
    string urlPerfil = "http://localhost:8080/perfil/";

    string fotoPerfilURL = "";
    string fotoMascotaURL = "";

    // =========================
    // START
    // =========================

    void Start()
    {
        int idUsuario =
            PlayerPrefs.GetInt("id_usuario");

        StartCoroutine(
            CargarDatosActuales(idUsuario)
        );
    }

    // =========================
    // CARGAR DATOS ACTUALES
    // =========================

    IEnumerator CargarDatosActuales(int id)
    {
        UnityWebRequest request =
            UnityWebRequest.Get(urlPerfil + id);

        yield return request.SendWebRequest();

        if (request.result ==
            UnityWebRequest.Result.Success)
        {
            Debug.Log(request.downloadHandler.text);

            DatosPerfilEditar datos =
                JsonUtility.FromJson<DatosPerfilEditar>(
                    request.downloadHandler.text
                );

            // -------------------------
            // NOMBRES
            // -------------------------

            inputNombreUsuario.text =
                datos.usuario_nombre;

            inputNombreMascota.text =
                datos.mascota_nombre;

            // -------------------------
            // URLS
            // -------------------------

            fotoPerfilURL =
                datos.foto_perfil;

            fotoMascotaURL =
                datos.foto_url;

            // -------------------------
            // FOTO PERFIL
            // -------------------------

            if (!string.IsNullOrEmpty(datos.foto_perfil))
            {
                StartCoroutine(
                    DescargarImagen(
                        datos.foto_perfil,
                        true
                    )
                );
            }

            // -------------------------
            // FOTO MASCOTA
            // -------------------------

            if (!string.IsNullOrEmpty(datos.foto_url))
            {
                StartCoroutine(
                    DescargarImagen(
                        datos.foto_url,
                        false
                    )
                );
            }
        }
        else
        {
            Debug.LogError(
                "Error cargando perfil: " +
                request.error
            );
        }
    }

    // =========================
    // DESCARGAR IMAGENES
    // =========================

    IEnumerator DescargarImagen(
        string url,
        bool esPerfil
    )
    {
        UnityWebRequest request =
            UnityWebRequestTexture.GetTexture(url);

        yield return request.SendWebRequest();

        if (request.result ==
            UnityWebRequest.Result.Success)
        {
            Texture textura =
                DownloadHandlerTexture.GetContent(
                    request
                );

            if (esPerfil)
            {
                Debug.Log("CARGANDO FOTO PERFIL");

                imagenPerfil.texture =
                    textura;

                imagenPerfil.color =
                    Color.white;
            }
            else
            {
                Debug.Log("CARGANDO FOTO MASCOTA");

                imagenMascota.texture =
                    textura;

                imagenMascota.color =
                    Color.white;
            }
        }
        else
        {
            Debug.LogError(
                "Error descargando imagen: " +
                request.error
            );
        }
    }

    // =========================
    // FOTO PERFIL
    // =========================

    public void SeleccionarFotoPerfil()
    {
        string path =
            UnityEditor.EditorUtility.OpenFilePanel(
                "Seleccionar imagen perfil",
                "",
                "png,jpg,jpeg"
            );

        if (!string.IsNullOrEmpty(path))
        {
            StartCoroutine(
                SubirImagen(path, true)
            );
        }
    }

    // =========================
    // FOTO MASCOTA
    // =========================

    public void SeleccionarFotoMascota()
    {
        string path =
            UnityEditor.EditorUtility.OpenFilePanel(
                "Seleccionar imagen mascota",
                "",
                "png,jpg,jpeg"
            );

        if (!string.IsNullOrEmpty(path))
        {
            StartCoroutine(
                SubirImagen(path, false)
            );
        }
    }

    // =========================
    // SUBIR IMAGEN
    // =========================

    IEnumerator SubirImagen(
        string path,
        bool esPerfil
    )
    {
        byte[] imageBytes =
            File.ReadAllBytes(path);

        WWWForm form =
            new WWWForm();

        form.AddBinaryData(
            "imagen",
            imageBytes,
            Path.GetFileName(path),
            "image/jpeg"
        );

        UnityWebRequest request =
            UnityWebRequest.Post(
                urlSubir,
                form
            );

        yield return request.SendWebRequest();

        if (request.result ==
            UnityWebRequest.Result.Success)
        {
            Debug.Log(
                request.downloadHandler.text
            );

            RespuestaImagen respuesta =
                JsonUtility.FromJson<RespuestaImagen>(
                    request.downloadHandler.text
                );

            Texture2D texture =
                new Texture2D(2, 2);

            texture.LoadImage(imageBytes);

            if (esPerfil)
            {
                fotoPerfilURL =
                    respuesta.url;

                imagenPerfil.texture =
                    texture;

                imagenPerfil.color =
                    Color.white;

                Debug.Log(
                    "Foto perfil actualizada"
                );
            }
            else
            {
                fotoMascotaURL =
                    respuesta.url;

                imagenMascota.texture =
                    texture;

                imagenMascota.color =
                    Color.white;

                Debug.Log(
                    "Foto mascota actualizada"
                );
            }
        }
        else
        {
            Debug.LogError(
                "Error subiendo imagen: " +
                request.error
            );
        }
    }

    // =========================
    // GUARDAR PERFIL
    // =========================

    public void GuardarPerfil()
    {
        StartCoroutine(
            EditarPerfil()
        );
    }

    IEnumerator EditarPerfil()
    {
        DatosEditarPerfil datos =
            new DatosEditarPerfil();

        datos.id_usuario =
            PlayerPrefs.GetInt(
                "id_usuario"
            );

        // -------------------------
        // EVITAR VACÍOS
        // -------------------------

        datos.nombre_usuario =
            string.IsNullOrEmpty(
                inputNombreUsuario.text
            )
            ? ""
            : inputNombreUsuario.text;

        datos.nombre_mascota =
            string.IsNullOrEmpty(
                inputNombreMascota.text
            )
            ? ""
            : inputNombreMascota.text;

        datos.foto_perfil =
            fotoPerfilURL;

        datos.foto_mascota =
            fotoMascotaURL;

        string json =
            JsonUtility.ToJson(datos);

        Debug.Log(
            "JSON enviado:"
        );

        Debug.Log(json);

        UnityWebRequest request =
            new UnityWebRequest(
                urlEditar,
                "POST"
            );

        byte[] bodyRaw =
            Encoding.UTF8.GetBytes(
                json
            );

        request.uploadHandler =
            new UploadHandlerRaw(
                bodyRaw
            );

        request.downloadHandler =
            new DownloadHandlerBuffer();

        request.SetRequestHeader(
            "Content-Type",
            "application/json"
        );

        yield return request.SendWebRequest();

        Debug.Log(
            request.downloadHandler.text
        );

        if (request.responseCode == 200)
        {
            mensajeTexto.text =
                "Perfil actualizado correctamente";

            if (uiEditarPerfil != null)
            {
                uiEditarPerfil.CerrarTodosLosPaneles();
            }
        }
        else
        {
            mensajeTexto.text =
                "Error actualizando perfil";

            Debug.LogError(
                "Error HTTP: " +
                request.responseCode
            );
        }
    }
}