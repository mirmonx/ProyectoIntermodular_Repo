const { Router } = require("express");
const router = Router(); //es el router donde voy guardando mis rutas

// Rutas usando GET
router.get("/", (req, res) => {
  console.log("Alguien ha iniciado la app"); //muestro por consola un mensaje
  res.send("Hola, bienvenido, soy tu agente virtual que te acompañará"); //esto es lo que se ve en el front de mi pagina
});

router.get("/inicio", (req, res) => {
  console.log("acaban de acceder a inicio");
  res.send("Encantado de vernos de nuevo");
});

router.get("/perfilMascota", (req, res) => {
  console.log("acaban de acceder al perfil de su mascota");
  res.send("Tu mascota está pendiente de su desparasitación.");
});

router.get("/historialMedico", (req, res) => {
  console.log("Acaban de acceder a su historial médico");
  res.send("Tu mascota tiene 22 anotaciones médicas");
});

router.get("/deteccionMascota", (req, res) => {
  console.log("Acaban de acceder al juego de deteccion de mascota");
  res.send("Que mascota vamos a detectar, perro o gato?");
});

//pongo 5 rutas post como pide el ejercicio
//la primera por ejemplo para recibir cuando alguien ha hecho login
router.post("/holaPOST", (req, res) => {
  console.log(req.body);
  res.json({ msg: "Login recibido" });
});

//otra para cuando hace un registro de una mascota
router.post("/registro", (req, res) => {
  console.log(req.body);
  res.json({ msg: "Mascota inscrita con exito" });
});

//para que diga cuando ha enviado mensaje
router.post("/contacto", (req, res) => {
  console.log(req.body);
  res.json({ msg: "Mensaje sobre su mascota recibido con exito" });
});

//lo mismo para salud nueva de una mascota
router.post("/comentario", (req, res) => {
  console.log(req.body);
  res.json({ msg: "Nuevos datos en la salud de la mascota" });
});

//para cuando recibo un nuevo animal en mi pagina
router.post("/pedido", (req, res) => {
  console.log(req.body);
  res.json({ msg: "Animal nuevo recibido" });
});

module.exports = router; //exporto las rutas para que las pueda usar
