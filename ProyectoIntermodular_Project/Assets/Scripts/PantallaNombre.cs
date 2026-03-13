//Script pantalla Nombre
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PantallaNombre : MonoBehaviour
{
    public TMP_InputField inputNombre;
    public TextMeshProUGUI textoTitulo;

    public static string nombreMascota;

    public void GuardarNombre()
    {
        nombreMascota = inputNombre.text;

        textoTitulo.text = "Hola!!! Encantado/a de conocerte " + nombreMascota;

        Debug.Log("Nombre introducido: " + nombreMascota);

        StartCoroutine(CambiarEscena()); //funcion para ejecutar y pausar el juego
    }

    IEnumerator CambiarEscena() //he puesto IENUMERATOR porque es una funcion que se ejecuta por partes 
    {
        yield return new WaitForSeconds(3f); //yield significa pausar el scrit pero sin congelar (sigue funcionando el juego)
        SceneManager.LoadScene("GatoOperro");// La escena en naranja, es la escena de Unity que abre
    }
}