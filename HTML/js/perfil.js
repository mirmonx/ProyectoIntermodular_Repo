window.onload = cargarPerfil;

async function cargarPerfil() {
  // =========================================
  // ID USUARIO
  // =========================================

  const id = localStorage.getItem("id_usuario");

  // SI NO HAY LOGIN
  if (!id) {
    window.location.href = "login.html";

    return;
  }

  try {
    // =========================================
    // PETICIÓN BACKEND
    // =========================================

    const response = await fetch("http://localhost:8080/perfil/" + id);

    const data = await response.json();

    console.log(data);

    // =========================================
    // NOMBRE USUARIO
    // =========================================

    document.getElementById("nombreUsuario").innerText =
      "Hola, " + data.usuario_nombre + " 🐾";

    // =========================================
    // NOMBRE MASCOTA
    // =========================================

    if (data.mascota_nombre) {
      document.getElementById("nombreMascota").innerText = data.mascota_nombre;
    }

    // =========================================
    // INFO MASCOTA
    // =========================================

    let edad = data.edad || "";

    document.getElementById("infoMascota").innerText = edad;

    // =========================================
    // FOTO MASCOTA
    // =========================================

    if (data.foto_url) {
      document.getElementById("fotoMascota").src = data.foto_url;
    }

    // =========================================
    // FOTO PERFIL
    // =========================================

    if (data.foto_perfil) {
      document.getElementById("fotoPerfil").src = data.foto_perfil;
    }
  } catch (error) {
    console.error("Error cargando perfil:", error);
  }
}
function abrirPetfil() {
  window.location.href = "petfil.html";
}
