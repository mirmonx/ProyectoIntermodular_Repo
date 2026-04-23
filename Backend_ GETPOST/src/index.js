const express = require("express"); //creo el servidor
const app = express(); //creo la aplicacion
const morgan = require("morgan"); //para ver por consola
const cors = require("cors"); //para conectarme con unity

app.set("port", process.env.PORT || 8080); //puerto

app.use(cors()); //peticiones externas (unity)
app.use(morgan("dev")); //muestra por consola esas peticiones
app.use(express.json()); //permite recibir datos modo json
app.use(express.urlencoded({ extended: true })); //permite recibir datos de formularios

// conecta con mi archivo de rutas
app.use(require("./routes/indexRoutes"));

//inicio servidor
app.listen(app.get("port"), () => {
  console.log("Servidor funcionando en puerto: " + app.get("port"));
});
