const mysql = require("mysql");

const db = mysql.createConnection({
  host: "sql7.freesqldatabase.com",
  user: "sql7825597",
  password: "WAbgVm4Lds",
  database: "sql7825597",
  port: 3306,
});

db.connect((err) => {
  if (err) console.log("Error conexión:", err);
  else console.log("Conectado a MySQL");
});

module.exports = db; // Esto permite que otros archivos usen la conexión
