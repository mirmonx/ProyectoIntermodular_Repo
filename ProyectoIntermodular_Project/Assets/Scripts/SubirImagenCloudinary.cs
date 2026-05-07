using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.IO;

//abre explorador archivos, selecciona imagen, la sube al backend, recibe URL Cloudinary y guarda URL en DatosMascota.foto_url

public class SubirImagenCloudinary : MonoBehaviour
{
    string url = "http://localhost:8080/subir-foto";

    public void SeleccionarImagen()
    {
        string path = UnityEditor.EditorUtility.OpenFilePanel(
            "Seleccionar imagen",
            "",
            "png,jpg,jpeg"
        );

        if (!string.IsNullOrEmpty(path))
        {
            StartCoroutine(SubirImagen(path));
        }
    }

    IEnumerator SubirImagen(string path)
    {
        byte[] imageBytes = File.ReadAllBytes(path);

        WWWForm form = new WWWForm();

        form.AddBinaryData(
            "imagen",
            imageBytes,
            Path.GetFileName(path),
            "image/jpeg"
        );

        UnityWebRequest request =
            UnityWebRequest.Post(url, form);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Imagen subida: " + request.downloadHandler.text);

            RespuestaImagen respuesta =
                JsonUtility.FromJson<RespuestaImagen>(
                    request.downloadHandler.text
                );

            DatosMascota.foto_url = respuesta.url;

            Debug.Log("URL guardada: " + DatosMascota.foto_url);
        }
        else
        {
            Debug.LogError(request.error);
        }
    }
}

[System.Serializable]
public class RespuestaImagen
{
    public string url;
}