using TMPro;
using UnityEngine;

public class PantallaPerro : MonoBehaviour
{
    public TMP_Text textoRaza;

    public void PerroPequeño()
    {
        textoRaza.text = "Tu perro es de raza pequeña.";
    }

    public void PerroMediano()
    {
        textoRaza.text = "Tu perro es de raza mediana.";
    }

    public void PerroGrande()
    {
        textoRaza.text = "Tu perro es de raza grande.";
    }
}
