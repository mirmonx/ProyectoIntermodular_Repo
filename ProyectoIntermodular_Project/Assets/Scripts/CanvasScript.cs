using TMPro;
using UnityEditor;
using UnityEngine;

public class CanvasScript : MonoBehaviour
{
    public GameObject menuPortada;
    public TMP_Text textWelcome;

    void Start()
    {
        menuPortada.SetActive(true);
    }

    public void Welcome()
    {      
        textWelcome.text = "¡Bienvenido a nuestra aplicación!";
    }

}
