const express = require("express");
const app = express(); //cargo express y crea nuestra app
const morgan = require("morgan");

app.set("port", process.env.PORT || 8080); //para usar el puerto que le digo

app.use(morgan("dev"));

app.use(require("./indexRoutes")); //conecto con las rutas que le he dado en el otro archivo y las usamos en la app

// enciendo el servidor
app.listen(app.get("port"), () => {
  console.log("hola desde el puerto " + app.get("port")); //muestro por pantalla que el puerto esta funcionando con un hola
});
