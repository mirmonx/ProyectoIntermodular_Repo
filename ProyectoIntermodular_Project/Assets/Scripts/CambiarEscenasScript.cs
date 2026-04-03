using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarEscenas : MonoBehaviour
{
    public void Ir_LogIn()
    {
        SceneManager.LoadScene("Login");
}
    public void Ir_Registrar()
    {
        SceneManager.LoadScene("Registrar");
    }

    public void Ir_GatoOPerro()
    {
        SceneManager.LoadScene("GatoOPerro");
    }

    public void Ir_NombreMascota()
    {
        SceneManager.LoadScene("NombreMascota");
    }

    public void Ir_PantallaGato()
    {
        SceneManager.LoadScene("PantallaGato");
    }

    public void Ir_PantallaPerro()
    {
        SceneManager.LoadScene("PantallaPerro");
    }

    public void Ir_Home() //Página principal Guess Mew
    {
        SceneManager.LoadScene("Home");
    }
}