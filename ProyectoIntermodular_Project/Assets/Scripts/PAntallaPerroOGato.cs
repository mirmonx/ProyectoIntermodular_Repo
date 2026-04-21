using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PantallaPerroOGato : MonoBehaviour
{
    public TMP_Text textoSaludo;

    void Start()
    {
        if (textoSaludo != null)
        {
            if (!string.IsNullOrEmpty(DatosMascota.nombre))
                textoSaludo.text = "Hola " + DatosMascota.nombre;
            else
                textoSaludo.text = "Hola!";
        }
    }

    public void SeleccionarGato()
    {
        SeleccionarAnimal("gato", "PantallaGato");
    }

    public void SeleccionarPerro()
    {
        SeleccionarAnimal("perro", "PantallaPerro");
    }

    void SeleccionarAnimal(string tipo, string escena)
    {
        DatosMascota.tipo = tipo;
        SceneManager.LoadScene(escena);
    }
}