const { Router } = require("express");
const router = Router();
const db = require("../config/db");

// --- 1. RUTA DE LOGIN ---
router.post("/login", (req, res) => {
  const { email, password } = req.body;
  console.log("Intento de login con:", email);

  const sqlUsuario = "SELECT * FROM Usuarios WHERE email = ? AND password = ?";

  db.query(sqlUsuario, [email, password], (err, users) => {
    if (err) {
      console.error("ERROR EN TABLA USUARIOS:", err.sqlMessage);
      return res.status(500).json({ error: err.sqlMessage });
    }

    if (users.length > 0) {
      const usuario = users[0];
      const userId = usuario.Id || usuario.id;

      // Buscamos si tiene algo en MASCOTAS
      const sqlMascota = "SELECT * FROM MASCOTAS WHERE usuario_id = ?";
      db.query(sqlMascota, [userId], (errM, pets) => {
        if (errM) {
          console.error("ERROR EN TABLA MASCOTAS:", errM.sqlMessage);
          return res.status(500).json({ error: errM.sqlMessage });
        }

        const tieneMascota = pets.length > 0;
        console.log(
          `¡Login!: ${usuario.nombre} | ¿Tiene mascota?: ${tieneMascota}`,
        );

        res.status(200).json({
          id: userId,
          nombre: usuario.nombre,
          email: usuario.email,
          tieneMascota: tieneMascota,
        });
      });
    } else {
      console.log("Credenciales incorrectas para:", email);
      res.status(401).json({ mensaje: "Usuario no encontrado" });
    }
  });
});

// --- 2. RUTA DE REGISTRO ---
router.post("/registro", (req, res) => {
  const { nombre, email, password } = req.body;
  console.log("Intentando registrar a:", nombre);

  const sql = "INSERT INTO Usuarios (nombre, email, password) VALUES (?, ?, ?)";

  db.query(sql, [nombre, email, password], (err, result) => {
    if (err) {
      console.error("Error al registrar:", err.sqlMessage);
      return res.status(500).json({ error: err.sqlMessage });
    }

    console.log("¡Usuario registrado! Nuevo ID:", result.insertId);
    res.status(201).json({
      mensaje: "Usuario creado",
      id: result.insertId,
    });
  });
});

// --- 3. RUTA DE PERFIL (Para la escena Home) ---
router.get("/perfil/:id", (req, res) => {
  const userId = req.params.id;
  console.log("Cargando perfil para ID:", userId);

  const sql = `
        SELECT u.nombre AS usuario_nombre, m.nombre AS mascota_nombre, m.ID AS mascota_id, m.edad
        FROM Usuarios u
        LEFT JOIN MASCOTAS m ON u.Id = m.usuario_id
        WHERE u.Id = ?
    `;

  db.query(sql, [userId], (err, result) => {
    if (err) {
      console.error("Error en perfil:", err.sqlMessage);
      return res.status(500).json({ error: err.sqlMessage });
    }

    if (result.length > 0) {
      res.status(200).json(result[0]);
    } else {
      res.status(404).json({ mensaje: "Usuario no encontrado" });
    }
  });
});

// --- 4. RUTA GUARDAR MASCOTA ---
router.post("/guardar-mascota", (req, res) => {
  const { nombre, tipo_mascota, usuario_id, pelaje, tamano, edad } = req.body;

  const sql = `INSERT INTO MASCOTAS (nombre, tipo_mascota, usuario_id, pelaje, tamano, edad) 
                 VALUES (?, ?, ?, ?, ?, ?)`;

  db.query(
    sql,
    [nombre, tipo_mascota, usuario_id, pelaje, tamano, edad],
    (err, result) => {
      if (err) return res.status(500).json({ error: err.sqlMessage });
      res.status(201).json({ mensaje: "Mascota creada", id: result.insertId });
    },
  );
});

// --- 5. RUTA ACTUALIZAR EDAD ---

router.post("/actualizar-edad", (req, res) => {
  const { id, edad } = req.body; // 'edad' ahora será un string como "5 meses"
  const sql = "UPDATE MASCOTAS SET edad = ? WHERE ID = ?";

  db.query(sql, [edad, id], (err, result) => {
    if (err) {
      console.error("Error al actualizar edad:", err.sqlMessage);
      return res.status(500).json({ error: err.sqlMessage });
    }
    res.status(200).json({ mensaje: "Edad actualizada correctamente" });
  });
});

module.exports = router;
