using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.SceneManagement;

[System.Serializable]
public class DatosPerfil
{
    public string usuario_nombre;
    public string mascota_nombre;
    public int mascota_id;
    public string edad;
}

public class PerfilScript : MonoBehaviour
{
    public TMP_Text textoUsuario;
    public TMP_Text textoMascota;

    string urlBase = "http://localhost:8080/perfil/";

    void Start()
    {
        // 1. Recuperamos el ID del usuario logueado
        int idGuardado = PlayerPrefs.GetInt("usuario_id");

        if (idGuardado != 0)
        {
            StartCoroutine(CargarPerfil(idGuardado));
        }
        else
        {
            Debug.LogError("No se encontró usuario_id. ¿Has hecho login?");
        }
    }

    IEnumerator CargarPerfil(int id)
    {
        UnityWebRequest request = UnityWebRequest.Get(urlBase + id);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            // 2. Leemos los datos del servidor
            DatosPerfil datos = JsonUtility.FromJson<DatosPerfil>(request.downloadHandler.text);

            // 3. Actualizamos la interfaz
            if (textoUsuario != null) textoUsuario.text = "¡Hola, " + datos.usuario_nombre + "!";
            if (textoMascota != null) textoMascota.text = datos.mascota_nombre;

            // 4. Guardamos TODO en la clase estática DatosMascota
            DatosMascota.nombre = datos.mascota_nombre;

            // Si la edad viene nula de SQL, la convertimos en "" para que no de error
            DatosMascota.edad = string.IsNullOrEmpty(datos.edad) ? "" : datos.edad;

            // Guardamos el ID de la mascota para el UPDATE de la edad después
            PlayerPrefs.SetInt("mascota_id", datos.mascota_id);
            PlayerPrefs.Save();

            Debug.Log("Perfil cargado. Mascota: " + DatosMascota.nombre + " Edad: " + DatosMascota.edad);
        }
        else
        {
            Debug.LogError("Error al cargar perfil: " + request.error);
        }
    }

    // Función vinculada al BOTÓN de la mascota
    public void AbrirDetalleMascota()
    {
        // 1. Forzamos que si es null sea una cadena vacía, y quitamos espacios
        string edadParaRevisar = "";
        if (DatosMascota.edad != null)
        {
            edadParaRevisar = DatosMascota.edad.Trim().ToLower();
        }

        Debug.Log("Intentando abrir detalle. Valor de edad: [" + edadParaRevisar + "]");

        // 2. Comprobamos todas las formas en las que la edad puede estar "vacía"
        if (string.IsNullOrEmpty(edadParaRevisar) ||
            edadParaRevisar == "null" ||
            edadParaRevisar == "" ||
            edadParaRevisar == "n/a") // Por si acaso pusiste N/A
        {
            Debug.Log("EDAD NO ENCONTRADA: Cargando escena de entrada de edad.");
            SceneManager.LoadScene("EdadMascota");
        }
        else
        {
            Debug.Log("EDAD ENCONTRADA: Cargando Petfil.");
            SceneManager.LoadScene("Petfil");
        }
    }
}