let fotoPerfilURL = "";

let fotoMascotaURL = "";

// ======================================
// CARGAR PERFIL
// ======================================

window.onload = cargarPerfil;

async function cargarPerfil() {
  const id = localStorage.getItem("id_usuario");

  if (!id) {
    window.location.href = "login.html";

    return;
  }

  const response = await fetch("http://localhost:8080/perfil/" + id);

  const data = await response.json();

  console.log(data);

  // ==========================
  // INPUTS
  // ==========================

  document.getElementById("nombreUsuario").value = data.usuario_nombre || "";

  document.getElementById("nombreMascota").value = data.mascota_nombre || "";

  // ==========================
  // FOTOS
  // ==========================

  if (data.foto_perfil) {
    fotoPerfilURL = data.foto_perfil;

    document.getElementById("previewPerfil").src = data.foto_perfil;
  }

  if (data.foto_url) {
    fotoMascotaURL = data.foto_url;

    document.getElementById("previewMascota").src = data.foto_url;
  }
}

// ======================================
// SUBIR FOTO PERFIL
// ======================================

document
  .getElementById("inputFotoPerfil")
  .addEventListener("change", async function (e) {
    const file = e.target.files[0];

    if (!file) return;

    const formData = new FormData();

    formData.append("imagen", file);

    const response = await fetch("http://localhost:8080/subir-foto", {
      method: "POST",
      body: formData,
    });

    const data = await response.json();

    console.log(data);

    fotoPerfilURL = data.url;

    document.getElementById("previewPerfil").src = data.url;
  });

// ======================================
// SUBIR FOTO MASCOTA
// ======================================

document
  .getElementById("inputFotoMascota")
  .addEventListener("change", async function (e) {
    const file = e.target.files[0];

    if (!file) return;

    const formData = new FormData();

    formData.append("imagen", file);

    const response = await fetch("http://localhost:8080/subir-foto", {
      method: "POST",
      body: formData,
    });

    const data = await response.json();

    console.log(data);

    fotoMascotaURL = data.url;

    document.getElementById("previewMascota").src = data.url;
  });

// ======================================
// GUARDAR PERFIL
// ======================================

async function guardarPerfil() {
  const id = localStorage.getItem("id_usuario");

  const nombreUsuario = document.getElementById("nombreUsuario").value;

  const nombreMascota = document.getElementById("nombreMascota").value;

  const response = await fetch("http://localhost:8080/editarPerfil", {
    method: "POST",

    headers: {
      "Content-Type": "application/json",
    },

    body: JSON.stringify({
      id_usuario: id,

      nombre_usuario: nombreUsuario,

      foto_perfil: fotoPerfilURL,

      nombre_mascota: nombreMascota,

      foto_mascota: fotoMascotaURL,
    }),
  });

  const data = await response.json();

  console.log(data);

  if (response.ok) {
    alert("Perfil actualizado");

    window.location.href = "perfil.html";
  } else {
    alert("Error actualizando perfil");
  }
}
