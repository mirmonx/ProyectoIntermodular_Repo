const express = require("express");
const app = express();
const morgan = require("morgan");

//Settings
app.set("port", process.env.PORT || 8080);

//Middlewares
app.use(morgan("dev"));

app.listen(app.get("port"), () => {
  console.log("hola desde el puerto", app.get("port"));
});

app.get("/rutaGet", (req, res) => {
  res.send("Hola, este es un Servidor GET");
});

app.use(require("./routes/index"));
