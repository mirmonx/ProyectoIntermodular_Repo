using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PantallaPerro : MonoBehaviour
{
    public TMP_Text textoRaza;

    public void PerroPequeño()
    {
        textoRaza.text = "Tu perro es pequeño.";
        DatosMascota.tamano = "pequeño";
        SceneManager.LoadScene("Petfil");
    }

    public void PerroMediano()
    {
        textoRaza.text = "Tu perro es mediano.";
        DatosMascota.tamano = "mediano";
        SceneManager.LoadScene("Petfil");
    }

    public void PerroGrande()
    {
        textoRaza.text = "Tu perro es grande.";
        DatosMascota.tamano = "grande";
        SceneManager.LoadScene("Petfil");
    }
}