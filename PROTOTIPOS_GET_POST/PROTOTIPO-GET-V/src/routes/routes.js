const {Router} = require("express");
const router = Router();


router.get("/", (req, res) => {
console.log("Alguien accedio al home");
res.send("Bienvenido al home");
});

router.get("/perfil", (req, res) => {
console.log("Alguien accedio a perfil get");
res.send("esta es la informacion del perfil");
});

router.get("/citas", (req, res) => {
console.log("Alguien accedio a citas get");
res.send("estas son las citas programadas");
});

router.get("/vacunas", (req, res) => {
console.log("Alguien accedio a vacunas get");
res.send("estas son las vacunas aplicadas");
});

router.get("/alimentacion", (req, res) => {
console.log("Alguien accedio a alimentacion get");
res.send("esta es la informacion de alimentacion");
});

router.post("/holaPOST", (req, res)=> {
    req.body = 
        "En el request body guardaremos los datos de los formularios para enviarlos a la base de datos";
    console.log("Enviada una peticion post.");
    console.log(req.body);
    // res.json({
    //     Title: "Probando cosas con un Post."
    // })
    res.send("hola soy la 1ª ruta POST.");
});

router.post("/postPerfil", (req, res) => {
  req.body = "Perfil guardado";

  console.log("Enviada una peticion post");
  console.log(req.body);
  res.send(req.body + " y ha sido añadido a la base de datos");
});

router.post("/postCita", (req, res) => {
  req.body = "Cita agendada.";

  console.log("Enviada una peticion post");
  console.log(req.body);
  res.send(req.body + " y ha sido añadida a la base de datos");
});

router.post("/postVacuna", (req, res) => {
  req.body = "Vacuna aplicada.";

  console.log("Enviada una peticion post");
  console.log(req.body);
  res.send(req.body + " y los quiere añadir a la base de datos");
});

router.post("/postAlimentacion", (req, res) => {
  req.body = "Alimentacion suministrada.";

  console.log("Enviada una peticion post");
  console.log(req.body);
  res.send(req.body + " y lo quiere añadir a la base de datos");
});

router.post("/postCerrarSesion", (req, res) => {
  req.body = "La sesion ha sido cerrada.";

  console.log("Enviada una peticion post");
  console.log(req.body);
  res.send(req.body + " y los quiere añadir a la base de datos");
});




module.exports = router;