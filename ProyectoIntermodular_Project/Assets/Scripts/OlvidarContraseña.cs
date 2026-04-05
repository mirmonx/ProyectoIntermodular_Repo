using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RecuperarPasswordScript : MonoBehaviour
{
    public TMP_InputField inputEmail;
    public TMP_InputField inputNuevaPassword;
    public TMP_InputField inputConfirmarPassword;
    public TMP_Text contraseñaGuardada;
    public TMP_Text contraseñaNoGuardada;

    private string rutaArchivo;

    void Awake()
    {
        rutaArchivo = $"{Application.persistentDataPath}/usuarios_bd.json";
    }

    public void GuardarNuevaPassword()
    {
        StartCoroutine(GuardarPasswordYVolver());
    }

    IEnumerator GuardarPasswordYVolver()
    {
        // 1. Validar campos vacíos
        if (string.IsNullOrEmpty(inputEmail.text) ||
            string.IsNullOrEmpty(inputNuevaPassword.text) ||
            string.IsNullOrEmpty(inputConfirmarPassword.text))
        {
            Debug.LogError("Recuperación fallida: Por favor, rellena todos los campos.");
            if (contraseñaNoGuardada != null)
                contraseñaGuardada.text = "";
            contraseñaNoGuardada.text = "";
            contraseñaNoGuardada.text = "Rellena todos los campos";
            yield break;
        }

        // 2. Validar coincidencia de contraseñas
        if (inputNuevaPassword.text != inputConfirmarPassword.text)
        {
            Debug.LogError("Recuperación fallida: Las contraseñas no coinciden.");
            if (contraseñaNoGuardada != null)
                contraseñaGuardada.text = "";
            contraseñaNoGuardada.text = "";
            contraseñaNoGuardada.text = "Las contraseñas no coinciden";
            yield break;
        }

        // 3. Validar longitud mínima
        if (inputNuevaPassword.text.Length < 4)
        {
            Debug.LogError("Recuperación fallida: La nueva contraseña debe tener al menos 4 caracteres.");
            if (contraseñaNoGuardada != null)
                contraseñaGuardada.text = "";
            contraseñaNoGuardada.text = "";
            contraseñaNoGuardada.text = "Mínimo 4 caracteres";
            yield break;
        }

        // 4. Comprobar si existe el archivo
        if (!File.Exists(rutaArchivo))
        {
            Debug.LogError("Error: No existe la base de datos de usuarios.");
            if (contraseñaNoGuardada != null)
                contraseñaGuardada.text = "";
            contraseñaNoGuardada.text = "";
            contraseñaNoGuardada.text = "No existe base de datos";
            yield break;
        }

        // 5. Leer JSON
        string contenidoJson = File.ReadAllText(rutaArchivo);
        ListaUsuarios bd = JsonUtility.FromJson<ListaUsuarios>(contenidoJson);

        bool encontrado = false;

        // 6. Buscar usuario por email
        foreach (Usuario u in bd.usuarios)
        {
            if (u.email == inputEmail.text)
            {
                encontrado = true;

                // 7. Cambiar contraseña
                u.password = inputNuevaPassword.text;

                // 8. Guardar cambios
                string nuevoJson = JsonUtility.ToJson(bd, true);
                File.WriteAllText(rutaArchivo, nuevoJson);

                Debug.Log($"Contraseña actualizada correctamente para el usuario con email: {u.email}");

                if (contraseñaGuardada != null)
                    contraseñaGuardada.text = "";
                contraseñaNoGuardada.text = "";
                contraseñaGuardada.text = "Contraseña actualizada correctamente";

                // 9. Esperar antes de volver al login
                yield return new WaitForSeconds(2f);

                // 10. Cargar escena Login
                SceneManager.LoadScene("Login");

                yield break;
            }
        }

        // 11. Si no encontró usuario
        if (!encontrado)
        {
            Debug.LogError("Recuperación fallida: No existe ningún usuario con ese email.");
            if (contraseñaNoGuardada != null)
                contraseñaGuardada.text = "";
            contraseñaNoGuardada.text = "";
            contraseñaNoGuardada.text = "No existe ese email";
        }
    }
}