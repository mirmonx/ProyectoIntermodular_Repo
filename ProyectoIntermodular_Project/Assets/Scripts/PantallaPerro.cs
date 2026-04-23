using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Collections;
using System.Text;

public class PantallaPerro : MonoBehaviour
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

    public TMP_Text textoRaza;

    string url = "https://rippling-sinless-margarita.ngrok-free.dev/guardarMascota";

    public void PerroPequeño()
    {
        textoRaza.text = "Tu perro es pequeño.";
        DatosMascota.tamano = "pequeño";
        DatosMascota.tipo = "perro";
        GuardarMascota();
    }

    public void PerroMediano()
    {
        textoRaza.text = "Tu perro es mediano.";
        DatosMascota.tamano = "mediano";
        DatosMascota.tipo = "perro";
        GuardarMascota();
    }

    public void PerroGrande()
    {
        textoRaza.text = "Tu perro es grande.";
        DatosMascota.tamano = "grande";
        DatosMascota.tipo = "perro";
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