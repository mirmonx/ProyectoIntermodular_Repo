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

    void Start()
    {
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
