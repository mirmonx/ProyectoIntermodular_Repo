using System.Collections.Generic;

// Esto permite que las clases se vean en otros scripts y se guarden en JSON
[System.Serializable]
public class Usuario
{
    public string nombre;
    public string email;
    public string password;
}

[System.Serializable]
public class ListaUsuarios
{
    public List<Usuario> usuarios = new List<Usuario>();
}