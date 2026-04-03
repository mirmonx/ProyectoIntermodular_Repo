using UnityEngine;
using TMPro;
using System.IO;
using System.Collections.Generic;

public class LoginScript : MonoBehaviour
{
    public TMP_InputField inputEmail;
    public TMP_InputField inputPassword;

    private string rutaArchivo;

    void Awake()
    {
        // Usamos la misma ruta y nombre de archivo que en el Registro
        rutaArchivo = $"{Application.persistentDataPath}/usuarios_bd.json";
    }

    public void IniciarSesion()
    {
        // 1. Validar campos vacíos
        if (string.IsNullOrEmpty(inputEmail.text) || string.IsNullOrEmpty(inputPassword.text))
        {
            Debug.LogError("Login fallido: Por favor, rellena todos los campos.");
            return;
        }

        // 2. Comprobar si existe el archivo de la base de datos
        if (!File.Exists(rutaArchivo))
        {
            Debug.LogError("Error: No hay ningún usuario registrado todavía.");
            return;
        }

        // 3. Cargar la base de datos (bd) desde el JSON
        string contenidoJson = File.ReadAllText(rutaArchivo);
        ListaUsuarios bd = JsonUtility.FromJson<ListaUsuarios>(contenidoJson);

        // 4. Buscar coincidencia en la lista
        bool encontrado = false;

        foreach (Usuario u in bd.usuarios)
        {
            // Comparamos los datos del JSON con los que el usuario escribió ahora
            if (u.email == inputEmail.text && u.password == inputPassword.text)
            {
                encontrado = true;
                Debug.Log($"¡Login exitoso! Bienvenido, {u.nombre}.");

                break; // Salimos del bucle porque ya encontramos al usuario
            }
        }

        // 5. Si terminó de buscar y no encontró nada
        if (!encontrado)
        {
            Debug.LogError("Login fallido: El email o la contraseña son incorrectos.");
        }
    }
}