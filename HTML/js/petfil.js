let idMascota = null;

window.onload = iniciar;

async function iniciar() {
  const idUsuario = localStorage.getItem("id_usuario");

  const response = await fetch("http://localhost:8080/perfil/" + idUsuario);

  const data = await response.json();

  console.log(data);

  idMascota = data.mascota_id;

  // FOTO

  if (data.foto_url) {
    document.getElementById("fotoMascota").src = data.foto_url;
  }

  // NOMBRE

  document.getElementById("nombreMascota").innerText = data.mascota_nombre;

  // EDAD

  document.getElementById("edadMascota").innerText = data.edad;

  // CARGAR DATOS

  cargarVacunas();

  cargarDesparasitacion();
}

// ======================================
// VACUNAS
// ======================================

async function guardarVacuna() {
  const texto = document.getElementById("inputVacuna").value;

  if (!texto) return;

  await fetch("http://localhost:8080/guardar-vacuna", {
    method: "POST",

    headers: {
      "Content-Type": "application/json",
    },

    body: JSON.stringify({
      id_mascota: idMascota,
      texto,
    }),
  });

  document.getElementById("inputVacuna").value = "";

  cargarVacunas();
}
document.getElementById("inputVacuna").value = "";

async function cargarVacunas() {
  const response = await fetch(
    "http://localhost:8080/vacunas-web/" + idMascota,
  );

  const data = await response.json();

  const lista = document.getElementById("listaVacunas");

  lista.innerHTML = "";

  const vacunas = (data.vacunas || "").split("\n");

  vacunas.forEach((vacuna) => {
    if (vacuna.trim() !== "") {
      lista.innerHTML += `
            <div class="item-lista">

                <span>
                    ${vacuna}
                </span>

                <button
                    onclick="borrarVacuna('${vacuna}')"
                    class="boton-borrar"
                >
                    ❌
                </button>

            </div>
            `;
    }
  });
}
async function borrarVacuna(vacuna) {
  const response = await fetch(
    "http://localhost:8080/vacunas-web/" + idMascota,
  );

  const data = await response.json();

  let vacunas = (data.vacunas || "").split("\n");

  vacunas = vacunas.filter((v) => v.trim() !== vacuna.trim());

  await fetch("http://localhost:8080/actualizar-vacunas", {
    method: "POST",

    headers: {
      "Content-Type": "application/json",
    },

    body: JSON.stringify({
      id_mascota: idMascota,
      vacunas: vacunas.join("\n"),
    }),
  });

  cargarVacunas();
}

// ======================================
// DESPARASITACIÓN
// ======================================

async function guardarDesparasitacion() {
  const texto = document.getElementById("inputDesparasitacion").value;

  if (!texto) return;

  await fetch("http://localhost:8080/guardar-desparasitacion", {
    method: "POST",

    headers: {
      "Content-Type": "application/json",
    },

    body: JSON.stringify({
      id_mascota: idMascota,
      texto,
    }),
  });

  document.getElementById("inputDesparasitacion").value = "";

  cargarDesparasitacion();
}
document.getElementById("inputDesparasitacion").value = "";

cargarDesparasitacion();

async function cargarDesparasitacion() {
  const response = await fetch(
    "http://localhost:8080/desparasitacion-web/" + idMascota,
  );

  const data = await response.json();

  const lista = document.getElementById("listaDesparasitacion");

  lista.innerHTML = "";

  const desparasitaciones = (data.desparasitacion || "").split("\n");

  desparasitaciones.forEach((item) => {
    if (item.trim() !== "") {
      lista.innerHTML += `
            <div class="item-lista">

                <span>
                    ${item}
                </span>

                <button
                    onclick='borrarDesparasitacion("${item}")'
                    class="boton-borrar"
                >
                    ❌
                </button>

            </div>
            `;
    }
  });
}
async function borrarDesparasitacion(desparasitacion) {
  console.log("BORRANDO:", desparasitacion);

  const response = await fetch(
    "http://localhost:8080/desparasitacion-web/" + idMascota,
  );

  const data = await response.json();

  let desparasitaciones = (data.desparasitacion || "").split("\n");

  desparasitaciones = desparasitaciones.filter(
    (d) => d.trim() !== desparasitacion.trim(),
  );

  await fetch("http://localhost:8080/actualizar-desparasitacion", {
    method: "POST",

    headers: {
      "Content-Type": "application/json",
    },

    body: JSON.stringify({
      id_mascota: idMascota,

      desparasitacion: desparasitaciones.join("\n"),
    }),
  });

  cargarDesparasitacion();
}
