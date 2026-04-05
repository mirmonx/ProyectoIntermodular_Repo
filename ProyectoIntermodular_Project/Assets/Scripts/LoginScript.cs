using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginScript : MonoBehaviour
{
    public TMP_InputField inputEmail;
    public TMP_InputField inputPassword;
    public TMP_Text contraseñaGuardada;
    public TMP_Text contraseñaNoGuardada;


    private string rutaArchivo;

    void Awake()
    {
        rutaArchivo = $"{Application.persistentDataPath}/usuarios_bd.json";
    }

    public void IniciarSesion()
    {
        StartCoroutine(IniciarSesionConEspera());
    }

    IEnumerator IniciarSesionConEspera()
    {
        // 1. Validar campos vacíos
        if (string.IsNullOrEmpty(inputEmail.text) || string.IsNullOrEmpty(inputPassword.text))
        {
            Debug.LogError("Login fallido: Por favor, rellena todos los campos.");
            if (contraseñaNoGuardada != null)
                contraseñaGuardada.text = "";
                contraseñaNoGuardada.text = "";
                contraseñaNoGuardada.text = "Rellena todos los campos";
            yield break;
        }

        // 2. Comprobar si existe el archivo
        if (!File.Exists(rutaArchivo))
        {
            Debug.LogError("Error: No hay ningún usuario registrado todavía.");
            if (contraseñaNoGuardada != null)
                contraseñaGuardada.text = "";
            contraseñaNoGuardada.text = "";
            contraseñaNoGuardada.text = "No hay usuarios registrados";
            yield break;
        }

        // 3. Leer la base de datos JSON
        string contenidoJson = File.ReadAllText(rutaArchivo);
        ListaUsuarios bd = JsonUtility.FromJson<ListaUsuarios>(contenidoJson);

        bool encontrado = false;

        // 4. Buscar usuario
        foreach (Usuario u in bd.usuarios)
        {
            if (u.email == inputEmail.text && u.password == inputPassword.text)
            {
                encontrado = true;
                Debug.Log($"¡Login exitoso! Bienvenido, {u.nombre}.");

                if (contraseñaGuardada != null)
                    contraseñaGuardada.text = "";
                contraseñaNoGuardada.text = "";
                contraseñaGuardada.text = "Login exitoso";

                // Esperamos 1 segundo antes de cambiar de escena
                yield return new WaitForSeconds(1f);

                // Cargamos la siguiente escena
                SceneManager.LoadScene("NombreMascota");

                yield break;
            }
        }

        // 5. Si no se encontró
        if (!encontrado)
        {
            Debug.LogError("Login fallido: El email o la contraseña son incorrectos.");
            if (contraseñaNoGuardada != null)
                contraseñaGuardada.text = "";
            contraseñaNoGuardada.text = "";
            contraseñaNoGuardada.text = "Email o contraseña incorrectos";
        }
    }
}