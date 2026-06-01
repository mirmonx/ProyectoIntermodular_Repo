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
  multipleStatements: true,
});

const sql = `
CREATE TABLE IF NOT EXISTS usuarios (
  id INT(4) NOT NULL AUTO_INCREMENT,
  nombre VARCHAR(30) NOT NULL,
  email VARCHAR(40) NOT NULL,
  password VARCHAR(20) NOT NULL,
  PRIMARY KEY (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE IF NOT EXISTS mascotas (
  id INT(11) NOT NULL AUTO_INCREMENT,
  nombre VARCHAR(20) NOT NULL,
  tipo_mascota VARCHAR(20) NOT NULL,
  edad VARCHAR(20) DEFAULT NULL,
  size VARCHAR(20) DEFAULT NULL,
  pelaje VARCHAR(20) DEFAULT NULL,
  vacunas VARCHAR(50) DEFAULT NULL,
  desparasitacion VARCHAR(50) DEFAULT NULL,
  alimentacion VARCHAR(50) DEFAULT NULL,
  aseo VARCHAR(50) DEFAULT NULL,
  citas VARCHAR(50) DEFAULT NULL,
  foto_url VARCHAR(200) DEFAULT NULL,
  id_usuario INT(4) NOT NULL,
  PRIMARY KEY (id),
  KEY REL1 (id_usuario),
  CONSTRAINT REL1 FOREIGN KEY (id_usuario)
    REFERENCES usuarios(id)
    ON DELETE CASCADE
    ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
`;

db.query(sql, (err) => {
  if (err) {
    console.error("Error creando tablas:", err);
  } else {
    console.log("Tablas creadas correctamente");
  }

  db.end();
});
