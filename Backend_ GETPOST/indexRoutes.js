const { Router } = require("express");
const router = Router();

const mysql = require("mysql");

const db = mysql.createConnection({
  host: "sql7.freesqldatabase.com",
  user: "sql7823808",
  password: "RM5wuCgC3D",
  database: "sql7823808",
  port: 3306,
});

db.connect((err) => {
  if (err) {
    console.log("Error conexión:", err);
  } else {
    console.log("Conectado a MySQL");
  }
});

// =======================
// LOGIN
// =======================
router.post("/login", (req, res) => {
  const { email, password } = req.body;

  const sql = "SELECT * FROM Usuarios WHERE email = ? AND password = ?";

  db.query(sql, [email, password], (err, result) => {
    if (err) return res.status(500).json(err);

    if (result.length === 0) {
      return res.status(401).json({ error: "Credenciales incorrectas" });
    }

    const usuario = result[0];

    console.log("ID REAL:", usuario.Id);

    const sqlMascota = "SELECT * FROM MASCOTAS WHERE usuario_id = ?";

    db.query(sqlMascota, [usuario.Id], (err2, mascotas) => {
      if (err2) return res.status(500).json(err2);

      res.json({
        id: usuario.Id,
        nombre: usuario.nombre,
        email: usuario.email,
        tieneMascota: mascotas.length > 0,
      });
    });
  });
});

// =======================
// REGISTRO
// =======================
router.post("/registro", (req, res) => {
  const { nombre, email, password } = req.body;

  const sql = `
    INSERT INTO Usuarios (nombre, email, password)
    VALUES (?, ?, ?)
  `;

  db.query(sql, [nombre, email, password], (err, result) => {
    if (err) return res.status(500).json(err);

    res.json({
      usuario_id: result.insertId,
    });
  });
});

// =======================
// GUARDAR MASCOTA
// =======================
router.post("/guardarMascota", (req, res) => {
  const { nombre, tipo, pelaje, tamano, usuario_id } = req.body;

  console.log("DATOS RECIBIDOS:", req.body);

  if (!nombre || !tipo || !usuario_id) {
    return res.status(400).json({ error: "Faltan datos" });
  }

  const sql = `
    INSERT INTO MASCOTAS (nombre, tipo_mascota, usuario_id, pelaje, tamano)
    VALUES (?, ?, ?, ?, ?)
  `;

  db.query(
    sql,
    [nombre, tipo, usuario_id, pelaje || null, tamano || null],
    (err, result) => {
      if (err) {
        console.log("ERROR BD:", err);
        return res.status(500).json(err);
      }

      res.json({ msg: "Mascota guardada" });
    },
  );
});

module.exports = router;
