using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PantallaNombre : MonoBehaviour
{
    public TMP_InputField inputNombre;
    public TMP_Text textoTitulo;

    public void GuardarNombre()
    {
        // guardamos el nombre en memoria
        DatosMascota.nombre = inputNombre.text;

        // mostramos mensaje
        textoTitulo.text = "Hola!!! " + DatosMascota.nombre;

        Debug.Log("Nombre guardado: " + DatosMascota.nombre);

        // pasamos a la siguiente pantalla
        SceneManager.LoadScene("GatoOPerro");
    }
}