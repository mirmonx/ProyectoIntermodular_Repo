using System.Collections; // <-- IMPORTANTE para IEnumerator
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking; // <-- IMPORTANTE para UnityWebRequest

public class Petfil : MonoBehaviour
{
    public RawImage fotoUI;
    public TMP_Text textoInfo;
    public TMP_Text textoMascota;

    public GameObject Vacunas;
    public TMP_InputField InputFieldVacuna;
    public TMP_Text Listavacunas;

    public GameObject Desparasitaciones;
    public TMP_InputField InputFieldDesparasitaciones;
    public TMP_Text ListaDesparasitaciones;

    public RawImage fotoMascotaDisplay;

    void Start()
    {
        // Aquí mostramos todo lo que hemos ido recolectando
        textoInfo.text = "Ficha de Mascota\n\n" +
                         "Nombre: " + DatosMascota.nombre + "\n" +
                         "Edad: " + DatosMascota.edad;

        Vacunas.SetActive(false);
        Desparasitaciones.SetActive(false);

        // Cargar foto si existe la URL
        if (!string.IsNullOrEmpty(DatosMascota.foto_url))
        {
            StartCoroutine(DescargarFoto(DatosMascota.foto_url));
        }
    }

    IEnumerator DescargarFoto(string url)
    {
        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(url))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                fotoUI.texture = DownloadHandlerTexture.GetContent(www);
            }
            else
            {
                Debug.LogError("Error al bajar la foto: " + www.error);
            }
        }
    }

    public void AbrirVacunas()
    {
        Vacunas.SetActive(true);
    }

    public void CerrarVacunas()
    {
        Vacunas.SetActive(false);
    }

    public void AbrirDesparasitaciones()
    {
        Desparasitaciones.SetActive(true);
    }

    public void CerrarDesparasitaciones()
    {
        Desparasitaciones.SetActive(false);
    }
}