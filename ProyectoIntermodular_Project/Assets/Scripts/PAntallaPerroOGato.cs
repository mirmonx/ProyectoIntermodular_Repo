using TMPro;
using UnityEngine;

public class PAntallaPerroOGato : MonoBehaviour
{
    public TMP_Text textoSaludo;

    void Start()
    {
        textoSaludo.text = "Hola " + PantallaNombre.nombreMascota; // muestra el nombre de la anterior pantalla
    }

    public void SeleccionarGato()
    {
        Debug.Log(PantallaNombre.nombreMascota + " es un gato");
    }

    public void SeleccionarPerro()
    {
        Debug.Log(PantallaNombre.nombreMascota + " es un perro");
    }
}