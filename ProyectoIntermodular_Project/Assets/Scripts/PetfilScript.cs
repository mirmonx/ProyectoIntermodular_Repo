using System.Globalization;
using TMPro;
using UnityEngine;

public class Petfil : MonoBehaviour
{
    public GameObject Vacunas;
    public TMP_InputField InputFieldVacuna;
    public TMP_Text Listavacunas;
    public GameObject Desparasitaciones;
    public TMP_InputField InputFieldDesparasitaciones;
    public TMP_Text ListaDesparasitaciones;
    public TMP_Text textoInfo;

    void Start()
    {
        // Aquí mostramos todo lo que hemos ido recolectando
        textoInfo.text = "Ficha de Mascota\n\n" +
                         "Nombre: " + DatosMascota.nombre + "\n" +
                         "Edad: " + DatosMascota.edad;
        Vacunas.SetActive(false);
        Desparasitaciones.SetActive(false);
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
