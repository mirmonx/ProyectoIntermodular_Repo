// ======================================
// GUARDAR MASCOTA
// ======================================

async function guardarMascota() {
  const nombre = document.getElementById("nombreMascota").value;

  const tipo = document.getElementById("tipo").value;

  const pelaje = document.getElementById("pelaje").value;

  const size = document.getElementById("size").value;

  const usuario_id = localStorage.getItem("id_usuario");

  let foto_url = "";

  // ==========================
  // SUBIR FOTO
  // ==========================

  const archivo = imagenInput.files[0];

  if (archivo) {
    const formData = new FormData();

    formData.append("imagen", archivo);

    const subida = await fetch("http://localhost:8080/subir-foto", {
      method: "POST",
      body: formData,
    });

    const dataFoto = await subida.json();

    foto_url = dataFoto.url;
  }

  // ==========================
  // GUARDAR MASCOTA
  // ==========================

  const response = await fetch("http://localhost:8080/guardar-mascota", {
    method: "POST",

    headers: {
      "Content-Type": "application/json",
    },

    body: JSON.stringify({
      nombre,
      tipo_mascota: tipo,
      usuario_id,
      pelaje,
      size,
      edad: "",
      foto_url,
    }),
  });

  const data = await response.json();

  console.log(data);

  if (response.ok) {
    alert("Mascota guardada");

    window.location.href = "perfil.html";
  } else {
    alert("Error guardando mascota");
  }
}
