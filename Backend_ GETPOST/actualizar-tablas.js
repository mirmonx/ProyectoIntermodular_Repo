require("dotenv").config();

const mysql = require("mysql2");

const db = mysql.createConnection({
  host: process.env.DB_HOST,
  user: process.env.DB_USER,
  password: process.env.DB_PASSWORD,
  database: process.env.DB_NAME,
  port: process.env.DB_PORT,
  ssl: {
    rejectUnauthorized: false,
  },
});

const sql = `
ALTER TABLE usuarios
ADD COLUMN foto_perfil VARCHAR(200) DEFAULT NULL;
`;

db.query(sql, (err) => {
  if (err) {
    console.error("Error actualizando tablas:", err);
  } else {
    console.log("Tabla usuarios actualizada correctamente");
  }

  db.end();
});
