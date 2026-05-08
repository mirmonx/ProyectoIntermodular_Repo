using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

[System.Serializable]
public class DatosTexto
{
    public int id_mascota;
    public string texto;
}

[System.Serializable]
public class RespuestaVacunas
{
    public string vacunas;
}

[System.Serializable]
public class RespuestaDesparasitacion
{
    public string desparasitacion;
}

public class PetfilScript : MonoBehaviour
{
    [Header("Info Mascota")]
    public RawImage fotoUI;
    public TMP_Text textoInfo;
    public TMP_Text textoMascota;

    [Header("Vacunas")]
    public GameObject Vacunas;
    public TMP_InputField InputFieldVacuna;
    public TMP_Text ListaVacunas;

    [Header("Desparasitaciones")]
    public GameObject Desparasitaciones;
    public TMP_InputField InputFieldDesparasitaciones;
    public TMP_Text ListaDesparasitaciones;

    [Header("Imágenes")]
    public RawImage fotoMascotaDisplay;
    public RawImage imagenPerfil;

    string urlBase = "http://localhost:8080/";

    int idMascota;

    void Start()
    {
        idMascota =
            PlayerPrefs.GetInt("mascota_id");

        // INFO MASCOTA
        textoInfo.text =
            "Ficha de Mascota\n\n" +
            "Nombre: " + DatosMascota.nombre + "\n" +
            "Edad: " + DatosMascota.edad;

        // PANELES CERRADOS
        Vacunas.SetActive(false);
        Desparasitaciones.SetActive(false);

        // FOTO
        if (!string.IsNullOrEmpty(DatosMascota.foto_url))
        {
            StartCoroutine(
                DescargarFoto(DatosMascota.foto_url)
            );
        }

        // CARGAR DATOS
        StartCoroutine(CargarVacunas());
        StartCoroutine(CargarDesparasitaciones());
    }

    // ==================================================
    // DESCARGAR FOTO
    // ==================================================

    IEnumerator DescargarFoto(string url)
    {
        using (
            UnityWebRequest www =
            UnityWebRequestTexture.GetTexture(url)
        )
        {
            yield return www.SendWebRequest();

            if (
                www.result ==
                UnityWebRequest.Result.Success
            )
            {
                Texture textura =
                    DownloadHandlerTexture.GetContent(www);

                fotoUI.texture =
                    textura;
            }
            else
            {
                Debug.LogError(
                    "Error al bajar la foto: " +
                    www.error
                );
            }
        }
    }

    // ==================================================
    // VACUNAS
    // ==================================================

    public void AbrirVacunas()
    {
        Vacunas.SetActive(true);
    }

    public void CerrarVacunas()
    {
        Vacunas.SetActive(false);
    }

    public void AñadirVacuna()
    {
        StartCoroutine(
            GuardarVacuna()
        );
    }

    IEnumerator GuardarVacuna()
    {
        if (
            string.IsNullOrEmpty(
                InputFieldVacuna.text
            )
        )
        {
            yield break;
        }

        DatosTexto datos =
            new DatosTexto();

        datos.id_mascota =
            idMascota;

        datos.texto =
            InputFieldVacuna.text;

        string json =
            JsonUtility.ToJson(datos);

        UnityWebRequest request =
            new UnityWebRequest(
                urlBase + "guardar-vacuna",
                "POST"
            );

        byte[] bodyRaw =
            Encoding.UTF8.GetBytes(json);

        request.uploadHandler =
            new UploadHandlerRaw(bodyRaw);

        request.downloadHandler =
            new DownloadHandlerBuffer();

        request.SetRequestHeader(
            "Content-Type",
            "application/json"
        );

        yield return request.SendWebRequest();

        if (
            request.result ==
            UnityWebRequest.Result.Success
        )
        {
            ListaVacunas.text +=
                "• " +
                InputFieldVacuna.text +
                "\n";

            InputFieldVacuna.text =
                "";
        }
        else
        {
            Debug.LogError(
                request.error
            );
        }
    }

    IEnumerator CargarVacunas()
    {
        UnityWebRequest request =
            UnityWebRequest.Get(
                urlBase +
                "vacunas/" +
                idMascota
            );

        yield return request.SendWebRequest();

        if (
            request.result ==
            UnityWebRequest.Result.Success
        )
        {
            Debug.Log(
                request.downloadHandler.text
            );

            RespuestaVacunas datos =
                JsonUtility.FromJson<RespuestaVacunas>(
                    request.downloadHandler.text
                );

            ListaVacunas.text =
                datos.vacunas;
        }
        else
        {
            Debug.LogError(
                request.error
            );
        }
    }

    // ==================================================
    // DESPARASITACIONES
    // ==================================================

    public void AbrirDesparasitaciones()
    {
        Desparasitaciones.SetActive(true);
    }

    public void CerrarDesparasitaciones()
    {
        Desparasitaciones.SetActive(false);
    }

    public void AñadirDesparasitacion()
    {
        StartCoroutine(
            GuardarDesparasitacion()
        );
    }

    IEnumerator GuardarDesparasitacion()
    {
        if (
            string.IsNullOrEmpty(
                InputFieldDesparasitaciones.text
            )
        )
        {
            yield break;
        }

        DatosTexto datos =
            new DatosTexto();

        datos.id_mascota =
            idMascota;

        datos.texto =
            InputFieldDesparasitaciones.text;

        string json =
            JsonUtility.ToJson(datos);

        UnityWebRequest request =
            new UnityWebRequest(
                urlBase +
                "guardar-desparasitacion",
                "POST"
            );

        byte[] bodyRaw =
            Encoding.UTF8.GetBytes(json);

        request.uploadHandler =
            new UploadHandlerRaw(bodyRaw);

        request.downloadHandler =
            new DownloadHandlerBuffer();

        request.SetRequestHeader(
            "Content-Type",
            "application/json"
        );

        yield return request.SendWebRequest();

        if (
            request.result ==
            UnityWebRequest.Result.Success
        )
        {
            ListaDesparasitaciones.text +=
                "• " +
                InputFieldDesparasitaciones.text +
                "\n";

            InputFieldDesparasitaciones.text =
                "";
        }
        else
        {
            Debug.LogError(
                request.error
            );
        }
    }

    IEnumerator CargarDesparasitaciones()
    {
        UnityWebRequest request =
            UnityWebRequest.Get(
                urlBase +
                "desparasitacion/" +
                idMascota
            );

        yield return request.SendWebRequest();

        if (
            request.result ==
            UnityWebRequest.Result.Success
        )
        {
            Debug.Log(
                request.downloadHandler.text
            );

            RespuestaDesparasitacion datos =
                JsonUtility.FromJson<RespuestaDesparasitacion>(
                    request.downloadHandler.text
                );

            ListaDesparasitaciones.text =
                datos.desparasitacion;
        }
        else
        {
            Debug.LogError(
                request.error
            );
        }
    }
}