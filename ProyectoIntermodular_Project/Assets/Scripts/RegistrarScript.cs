using UnityEngine;
using TMPro;
using System.IO;
using System.Collections.Generic;
using UnityEngine.UI;

// --- REGISTRAR SCRIPT ---
public class RegistrarScript : MonoBehaviour
{
    public TMP_InputField inputNombre;
    public TMP_InputField inputEmail;
    public TMP_InputField inputPassword;

    private string rutaArchivo;

    void Awake() //es un paso previo a Start()
    {
        // Definimos la ruta base al arrancar
        rutaArchivo = $"{Application.persistentDataPath}/usuarios_bd.json";
    }

    public void RegistrarUsuario()
    {
        // 1. Validar campos vacíos
        if (string.IsNullOrEmpty(inputNombre.text) ||
            string.IsNullOrEmpty(inputEmail.text) ||
            string.IsNullOrEmpty(inputPassword.text))
        {
            Debug.LogError("Registro fallido: Faltan campos por rellenar.");
            return;
        }

        // 2. Cargar base de datos existente
        ListaUsuarios bdActual = CargarBaseDeDatos(); //la bd será el json en nuestro caso

        // 3. Crear nuevo usuario y añadirlo
        Usuario nuevo = new Usuario();
        nuevo.nombre = inputNombre.text;
        nuevo.email = inputEmail.text;
        nuevo.password = inputPassword.text;

        bdActual.usuarios.Add(nuevo);

        // 4. Guardar la lista actualizada
        GuardarBaseDeDatos(bdActual);

        // 5. Finalizar
        LimpiarCampos();
    }

    private ListaUsuarios CargarBaseDeDatos()
    {
        if (File.Exists(rutaArchivo))
        {
            string json = File.ReadAllText(rutaArchivo);
            return JsonUtility.FromJson<ListaUsuarios>(json);
        }
        return new ListaUsuarios();
    }

    private void GuardarBaseDeDatos(ListaUsuarios bd)
    {
        string json = JsonUtility.ToJson(bd, true);

        // Usamos la sintaxis limpia que querías
        File.WriteAllText(rutaArchivo, json);

        Debug.Log($"¡Registro exitoso! Datos guardados en: {rutaArchivo}");
    }

    private void LimpiarCampos()
    {
        inputNombre.text = "";
        inputEmail.text = "";
        inputPassword.text = "";
    }
}