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
        SceneManager.LoadScene("Quizz");
    }

    public void Name()
    {
        SceneManager.LoadScene("PetName");
    }
     public void Home()
    {
        SceneManager.LoadScene("Home");
    }
}