const express = require('express');
const app = express();
const morgan = require("morgan");


//Settings
app.set('port', process.env.PORT || 8080);

//Middleware
app.use(morgan("dev"));

//Rutas
app.use(require("./routes/routes"));

//Iniciando el servidor
app.listen(app.get('port'), leerPuerto());

function leerPuerto() {
  console.log('hola desde el puerto', app.get('port'));
};




