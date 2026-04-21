const { Router } = require("express");
const router = Router();

const mysql = require("mysql");

// conexión a MySQL
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

//login
router.post("/login", (req, res) => {
  const { email, password } = req.body;

  const sqlUsuario = "SELECT * FROM Usuarios WHERE email = ? AND password = ?";

  db.query(sqlUsuario, [email, password], (err, result) => {
    if (err) return res.status(500).json(err);

    if (result.length === 0) {
      return res.status(401).json({ error: "Credenciales incorrectas" });
    }

    const usuario = result[0];

    console.log("Usuario desde BD:", usuario);

    // comprobar si tiene mascota
    const sqlMascota = "SELECT * FROM MASCOTAS WHERE usuario_id = ?";

    db.query(sqlMascota, [usuario.ID], (err2, mascotas) => {
      if (err2) return res.status(500).json(err2);

      res.json({
        id: usuario.ID,
        nombre: usuario.nombre,
        email: usuario.email,
        tieneMascota: mascotas.length > 0,
      });
    });
  });
});

module.exports = router;
