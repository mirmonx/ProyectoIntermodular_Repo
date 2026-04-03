using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{
    public void LogIn()
    {
        SceneManager.LoadScene("Login");
}
    public void Reigster()
    {
        SceneManager.LoadScene("Register");
    }

    public void Quizz()
    {
        SceneManager.LoadScene("GatoOperro");
    }

    public void Name()
    {
        SceneManager.LoadScene("NombreMascota");
    }
     public void Home()
    {
        SceneManager.LoadScene("Home");
    }
}