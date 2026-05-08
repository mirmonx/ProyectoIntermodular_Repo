const { Router } = require("express");
const router = Router();

const db = require("../config/db");

const multer = require("multer");
const upload = multer({ dest: "uploads/" });

const cloudinary = require("../config/cloudinary");
const fs = require("fs");

// ======================================================
// LOGIN
// ======================================================

router.post("/login", (req, res) => {
  const { email, password } = req.body;

  console.log("Intento de login con:", email);

  const sqlUsuario = "SELECT * FROM usuarios WHERE email = ? AND password = ?";

  db.query(sqlUsuario, [email, password], (err, users) => {
    if (err) {
      console.error("ERROR EN TABLA USUARIOS:", err.sqlMessage);

      return res.status(500).json({
        error: err.sqlMessage,
      });
    }

    if (users.length > 0) {
      const usuario = users[0];

      const userId = usuario.id;

      const sqlMascota = "SELECT * FROM mascotas WHERE id_usuario = ?";

      db.query(sqlMascota, [userId], (errM, pets) => {
        if (errM) {
          console.error("ERROR EN TABLA MASCOTAS:", errM.sqlMessage);

          return res.status(500).json({
            error: errM.sqlMessage,
          });
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

      res.status(401).json({
        mensaje: "Usuario no encontrado",
      });
    }
  });
});

// ======================================================
// REGISTRO
// ======================================================

router.post("/registro", (req, res) => {
  const { nombre, email, password } = req.body;

  console.log("Intentando registrar a:", nombre);

  const sql = "INSERT INTO usuarios (nombre, email, password) VALUES (?, ?, ?)";

  db.query(sql, [nombre, email, password], (err, result) => {
    if (err) {
      console.error("Error al registrar:", err.sqlMessage);

      return res.status(500).json({
        error: err.sqlMessage,
      });
    }

    console.log("¡Usuario registrado! Nuevo ID:", result.insertId);

    res.status(201).json({
      mensaje: "Usuario creado",
      id: result.insertId,
    });
  });
});

// ======================================================
// PERFIL
// ======================================================

router.get("/perfil/:id", (req, res) => {
  const userId = req.params.id;

  const sql = `
    SELECT 
      u.nombre AS usuario_nombre,
      u.foto_perfil,
      m.nombre AS mascota_nombre,
      m.id AS mascota_id,
      m.edad,
      m.foto_url
    FROM usuarios u
    LEFT JOIN mascotas m
    ON u.id = m.id_usuario
    WHERE u.id = ?
  `;

  db.query(sql, [userId], (err, result) => {
    if (err) {
      console.log(err);

      return res.status(500).json({
        error: err.sqlMessage,
      });
    }

    if (result.length > 0) {
      console.log("DATOS PERFIL ENVIADOS:");

      console.log(result[0]);

      res.status(200).json(result[0]);
    } else {
      res.status(404).json({
        mensaje: "Usuario no encontrado",
      });
    }
  });
});

// ======================================================
// GUARDAR MASCOTA
// ======================================================

router.post("/guardar-mascota", (req, res) => {
  const { nombre, tipo_mascota, usuario_id, pelaje, size, edad, foto_url } =
    req.body;

  const sql = `
    INSERT INTO mascotas
    (
      nombre,
      tipo_mascota,
      id_usuario,
      pelaje,
      size,
      edad,
      foto_url
    )
    VALUES (?, ?, ?, ?, ?, ?, ?)
  `;

  db.query(
    sql,
    [nombre, tipo_mascota, usuario_id, pelaje, size, edad, foto_url],
    (err, result) => {
      if (err) {
        console.log(err);

        return res.status(500).json({
          error: err.sqlMessage,
        });
      }

      res.status(201).json({
        mensaje: "Mascota creada",
        id: result.insertId,
      });
    },
  );
});

// ======================================================
// ACTUALIZAR EDAD
// ======================================================

router.post("/actualizar-edad", (req, res) => {
  const { id, edad } = req.body;

  const sql = "UPDATE mascotas SET edad = ? WHERE id = ?";

  db.query(sql, [edad, id], (err) => {
    if (err) {
      console.error("Error al actualizar edad:", err.sqlMessage);

      return res.status(500).json({
        error: err.sqlMessage,
      });
    }

    res.status(200).json({
      mensaje: "Edad actualizada correctamente",
    });
  });
});

// ======================================================
// SUBIR FOTO
// ======================================================

router.post("/subir-foto", upload.single("imagen"), async (req, res) => {
  try {
    const resultado = await cloudinary.uploader.upload(req.file.path);

    fs.unlinkSync(req.file.path);

    const fotoUrl = resultado.secure_url;

    console.log("FOTO SUBIDA A CLOUDINARY:");

    console.log(fotoUrl);

    res.status(200).json({
      mensaje: "Foto subida correctamente",
      url: fotoUrl,
    });
  } catch (error) {
    console.log(error);

    res.status(500).json({
      error: "Error al subir imagen",
    });
  }
});

// ======================================================
// EDITAR PERFIL
// ======================================================

router.post("/editarPerfil", (req, res) => {
  console.log("DATOS RECIBIDOS EDITAR PERFIL:");

  console.log(req.body);

  const {
    id_usuario,
    nombre_usuario,
    foto_perfil,
    nombre_mascota,
    foto_mascota,
  } = req.body;

  console.log("FOTO PERFIL:");
  console.log(foto_perfil);

  // ==================================================
  // ACTUALIZAR USUARIO
  // ==================================================

  const sqlUsuario = `
    UPDATE usuarios
    SET 
      nombre = ?,
      foto_perfil = ?
    WHERE id = ?
  `;

  db.query(sqlUsuario, [nombre_usuario, foto_perfil, id_usuario], (err) => {
    if (err) {
      console.log(err);

      return res.status(500).json({
        error: err.message,
      });
    }

    // ==============================================
    // ACTUALIZAR MASCOTA
    // ==============================================

    const sqlMascota = `
        UPDATE mascotas
        SET
          nombre = ?,
          foto_url = ?
        WHERE id_usuario = ?
      `;

    db.query(sqlMascota, [nombre_mascota, foto_mascota, id_usuario], (err2) => {
      if (err2) {
        console.log(err2);

        return res.status(500).json({
          error: err2.message,
        });
      }

      console.log("PERFIL ACTUALIZADO");

      res.json({
        mensaje: "Perfil actualizado",
      });
    });
  });
});
// ======================================================
// GUARDAR VACUNA
// ======================================================

router.post("/guardar-vacuna", (req, res) => {
  const { id_mascota, texto } = req.body;

  // primero obtenemos lo que ya existe
  const sqlBuscar = `
    SELECT vacunas
    FROM mascotas
    WHERE id = ?
  `;

  db.query(sqlBuscar, [id_mascota], (err, result) => {
    if (err) {
      console.log(err);

      return res.status(500).json({
        error: err.sqlMessage,
      });
    }

    let vacunasActuales = "";

    if (result.length > 0 && result[0].vacunas != null) {
      vacunasActuales = result[0].vacunas;
    }

    // añadimos nueva vacuna
    const nuevasVacunas = vacunasActuales + "• " + texto + "\n";

    const sqlUpdate = `
      UPDATE mascotas
      SET vacunas = ?
      WHERE id = ?
    `;

    db.query(sqlUpdate, [nuevasVacunas, id_mascota], (err2) => {
      if (err2) {
        console.log(err2);

        return res.status(500).json({
          error: err2.sqlMessage,
        });
      }

      res.status(200).json({
        mensaje: "Vacuna guardada",
      });
    });
  });
});

// ======================================================
// CARGAR VACUNAS
// ======================================================

router.get("/vacunas/:id", (req, res) => {
  const idMascota = req.params.id;

  const sql = `
    SELECT vacunas
    FROM mascotas
    WHERE id = ?
  `;

  db.query(sql, [idMascota], (err, result) => {
    if (err) {
      console.log(err);

      return res.status(500).json({
        error: err.sqlMessage,
      });
    }

    res.status(200).json({
      vacunas: result.length > 0 ? result[0].vacunas : "",
    });
  });
});

// ======================================================
// GUARDAR DESPARASITACIÓN
// ======================================================

router.post("/guardar-desparasitacion", (req, res) => {
  const { id_mascota, texto } = req.body;

  const sqlBuscar = `
    SELECT desparasitacion
    FROM mascotas
    WHERE id = ?
  `;

  db.query(sqlBuscar, [id_mascota], (err, result) => {
    if (err) {
      console.log(err);

      return res.status(500).json({
        error: err.sqlMessage,
      });
    }

    let desparasitacionActual = "";

    if (result.length > 0 && result[0].desparasitacion != null) {
      desparasitacionActual = result[0].desparasitacion;
    }

    const nuevaDesparasitacion = desparasitacionActual + "• " + texto + "\n";

    const sqlUpdate = `
      UPDATE mascotas
      SET desparasitacion = ?
      WHERE id = ?
    `;

    db.query(sqlUpdate, [nuevaDesparasitacion, id_mascota], (err2) => {
      if (err2) {
        console.log(err2);

        return res.status(500).json({
          error: err2.sqlMessage,
        });
      }

      res.status(200).json({
        mensaje: "Desparasitación guardada",
      });
    });
  });
});

// ======================================================
// CARGAR DESPARASITACIÓN
// ======================================================

router.get("/desparasitacion/:id", (req, res) => {
  const idMascota = req.params.id;

  const sql = `
    SELECT desparasitacion
    FROM mascotas
    WHERE id = ?
  `;

  db.query(sql, [idMascota], (err, result) => {
    if (err) {
      console.log(err);

      return res.status(500).json({
        error: err.sqlMessage,
      });
    }

    res.status(200).json({
      desparasitacion: result.length > 0 ? result[0].desparasitacion : "",
    });
  });
});
router.get("/vacunas-web/:id", (req, res) => {
  const idMascota = req.params.id;

  const sql = `
        SELECT vacunas
        FROM mascotas
        WHERE id = ?
    `;

  db.query(sql, [idMascota], (err, result) => {
    if (err) {
      return res.status(500).json({
        error: err.sqlMessage,
      });
    }

    res.json(result[0]);
  });
});
router.get("/desparasitacion-web/:id", (req, res) => {
  const idMascota = req.params.id;

  const sql = `
        SELECT desparasitacion
        FROM mascotas
        WHERE id = ?
    `;

  db.query(sql, [idMascota], (err, result) => {
    if (err) {
      return res.status(500).json({
        error: err.sqlMessage,
      });
    }

    res.json(result[0]);
  });
});
router.delete("/borrar-vacuna/:id", (req, res) => {
  const id = req.params.id;

  const sql = `
        DELETE FROM vacunas
        WHERE id = ?
    `;

  db.query(sql, [id], (err) => {
    if (err) {
      return res.status(500).json({
        error: err.sqlMessage,
      });
    }

    res.json({
      mensaje: "Vacuna eliminada",
    });
  });
});
router.post("/actualizar-vacunas", (req, res) => {
  const { id_mascota, vacunas } = req.body;

  const sql = `
        UPDATE mascotas
        SET vacunas = ?
        WHERE id = ?
    `;

  db.query(sql, [vacunas, id_mascota], (err) => {
    if (err) {
      console.log(err);

      return res.status(500).json({
        error: err.sqlMessage,
      });
    }

    res.json({
      mensaje: "Vacunas actualizadas",
    });
  });
});
router.post("/actualizar-desparasitacion", (req, res) => {
  const { id_mascota, desparasitacion } = req.body;

  const sql = `
        UPDATE mascotas
        SET desparasitacion = ?
        WHERE id = ?
    `;

  db.query(sql, [desparasitacion, id_mascota], (err) => {
    if (err) {
      console.log(err);

      return res.status(500).json({
        error: err.sqlMessage,
      });
    }

    res.json({
      mensaje: "Desparasitación actualizada",
    });
  });
});
module.exports = router;
