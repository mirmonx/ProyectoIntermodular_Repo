using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Collections;
using System.Text;

public class PantallaGato : MonoBehaviour
{
    [System.Serializable]
    public class MascotaEnviar
    {
        public string nombre;
        public string tipo;
        public string pelaje;
        public string tamano;
        public int usuario_id;
    }

    public TMP_Text textoPelo;

    string url = "https://rippling-sinless-margarita.ngrok-free.dev/guardarMascota";

    public void PeloCalvo()
    {
        textoPelo.text = "Tu gato es calvo.";
        DatosMascota.pelaje = "calvo";
        DatosMascota.tipo = "gato";
        GuardarMascota();
    }

    public void PeloCorto()
    {
        textoPelo.text = "Tu gato tiene el pelo corto.";
        DatosMascota.tipo = "gato";
        DatosMascota.pelaje = "corto";
        GuardarMascota();
    }

    public void PeloLargo()
    {
        textoPelo.text = "Tu gato tiene el pelo largo.";
        DatosMascota.pelaje = "largo";
        DatosMascota.tipo = "gato";
        GuardarMascota();
    }

    void GuardarMascota()
    {
        StartCoroutine(EnviarMascota());
    }

    IEnumerator EnviarMascota()
    {
        int usuario_id = PlayerPrefs.GetInt("usuario_id");

        Debug.Log("ID QUE ENVÍO: " + usuario_id);

        MascotaEnviar datos = new MascotaEnviar();
        datos.nombre = DatosMascota.nombre;
        datos.tipo = DatosMascota.tipo;
        datos.pelaje = DatosMascota.pelaje;
        datos.tamano = DatosMascota.tamano;
        datos.usuario_id = usuario_id;

        string json = JsonUtility.ToJson(datos);

      
        Debug.Log("JSON QUE ENVÍO: " + json);

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);

        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Mascota guardada OK");
            SceneManager.LoadScene("Petfil");
        }
        else
        {
            Debug.LogError("ERROR BACKEND: " + request.downloadHandler.text);
        }
    }
}