const { Router } = require("express");
const router = Router();

//Rutas GET
router.get("/", (req, res) => {
  console.log("Alguien accedió a Guess Mew");
  res.send("Bienvenidx a nuestra aplicación");
});

router.get("/name", (req, res) => {
  console.log("Alguien accedió a /name");
  res.send("Introduce el nombre de tu peludo:");
});

router.get("/catordog", (req, res) => {
  console.log("Alguien accedió a /catordog");
  res.send("¿Tienes un gatitio o un perrito?");
});

router.get("/furrcat", (req, res) => {
  console.log("Alguien accedió a /furrcat");
  res.send("¿Cuánto pelo tiene tu michi?");
});

router.get("/sizedog", (req, res) => {
  console.log("Alguien accedió a /sizedog");
  res.send("¿De qué tamaño es tu perrito?");
});

router.post("/holaPost", (req, res) => {
  req.body =
    "En el request body guardaremos los datos de los formularios para enviarlos a la base de datos";

  console.log("Enviada una peticion post");
  console.log(req.body);
  res.json({ Title: "Probando cosas con un POST" });
});

router.post("/post1", (req, res) => {
  req.body = "Pipo es un perrito";

  console.log("Enviada una peticion post");
  console.log(req.body);
  res.send(req.body + " y ha sido añadido a la base de datos");
});

router.post("/post2", (req, res) => {
  req.body = "Mina es una gatita";

  console.log("Enviada una peticion post");
  console.log(req.body);
  res.send(req.body + " y ha sido añadida a la base de datos");
});

router.post("/post3", (req, res) => {
  req.body = "Carlos tiene dos perros y un gato";

  console.log("Enviada una peticion post");
  console.log(req.body);
  res.send(req.body + " y los quiere añadir a la base de datos");
});

router.post("/post4", (req, res) => {
  req.body = "Pepe tiene que llevar a su perro al veterinario";

  console.log("Enviada una peticion post");
  console.log(req.body);
  res.send(req.body + " y lo quiere añadir a la base de datos");
});

router.post("/post5", (req, res) => {
  req.body = "Lola tiene tres gatos";

  console.log("Enviada una peticion post");
  console.log(req.body);
  res.send(req.body + " y los quiere añadir a la base de datos");
});

module.exports = router;
