const { Router } = require("express");
const router = Router(); // Router para guardar todas las rutas

//rutas GET

// pantalla inicial (gato o perro)
router.get("/inicio", (req, res) => {
  console.log("Acceso a pantalla inicio"); // mensaje en consola cuando alguien entra
  res.json({ mensaje: "¿Eres un gatito o un perrito?" }); // lo que se manda al frontend
});

// pantalla juego (más adelante abrirá cámara)
router.get("/juego", (req, res) => {
  console.log("Acceso al juego");
  res.json({ mensaje: "Bienvenido a Guess Mew" });
});

// pantalla introducir nombre mascota
router.get("/perfilMascota", (req, res) => {
  console.log("Acceso a perfil mascota");
  res.json({ mensaje: "Introduce el nombre de tu mascota" });
});

//rutas POST

// cuando el usuario selecciona gato o perro
router.post("/seleccionAnimal", (req, res) => {
  console.log("Animal seleccionado:", req.body);
  // aquí recibo algo como { animal: "gato" o "perro" }

  res.json({ msg: "Animal seleccionado correctamente" }); // respuesta al frontend
});

// cuando el usuario elige el tipo de gato (corto, mediano, largo)
router.post("/tipoGato", (req, res) => {
  console.log("Tipo de gato:", req.body);
  // { tipo: "corto" / "mediano" / "largo" }

  res.json({ msg: "Tipo de pelaje de gato guardado correctamente" });
});

// 🐶 cuando el usuario elige el tamaño del perro (pequeño, mediano, grande)
router.post("/tipoPerro", (req, res) => {
  console.log("Tamaño del perro:", req.body);
  // { tamano: "pequeño" / "mediano" / "grande" }

  res.json({ msg: "Tamaño del perro guardado correctamente" });
});

// cuando el usuario introduce el nombre de la mascota
router.post("/nombreMascota", (req, res) => {
  console.log("Nombre mascota:", req.body);
  // { nombre: "Luna" }

  res.json({ msg: "Nombre guardado correctamente" });
});

// guardo toda la información de la mascota junta (nombre + tipo + detalle)
router.post("/guardarMascota", (req, res) => {
  console.log("Mascota completa:", req.body);
  // { nombre: "Luna", tipo: "gato", detalle: "corto" }

  res.json({ msg: "Mascota guardada correctamente" });
});

// cuando el usuario crea una cuenta
router.post("/registro", (req, res) => {
  console.log("Registro usuario:", req.body);
  // { name: "...", email: "...", password: "..." }

  res.json({ msg: "Usuario registrado correctamente" });
});

// exporto el router para poder usarlo en js
module.exports = router;
