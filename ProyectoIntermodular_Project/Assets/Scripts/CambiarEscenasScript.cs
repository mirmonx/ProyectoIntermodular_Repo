using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CambiarEscenas : MonoBehaviour
{
    public void IrConRetraso()
    {
        StartCoroutine(EsperarYCambiar());
    }

    IEnumerator EsperarYCambiar()
    {
        yield return new WaitForSeconds(2f); // espera 2 segundos
        SceneManager.LoadScene("Login");
    }
    public void Ir_LogIn()
    {
        StartCoroutine(EsperarYCambiar());
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

    public void Ir_RecuperarContraseña()
    {
        SceneManager.LoadScene("RecuperarContraseña");
    }

    public void Ir_Perfil()
    {
        SceneManager.LoadScene("Perfil");
    }

    public void Ir_Petfil()
    { 
        SceneManager.LoadScene("Petfil");
    }

}