async function registro() {
  const nombre = document.getElementById("nombre").value;

  const email = document.getElementById("email").value;

  const password = document.getElementById("password").value;

  const confirmPassword = document.getElementById("confirmPassword").value;

  // VALIDACION

  if (!nombre || !email || !password) {
    alert("Completa todos los campos");

    return;
  }

  // CONTRASEÑAS

  if (password !== confirmPassword) {
    alert("Las contraseñas no coinciden");

    return;
  }

  try {
    const response = await fetch("http://localhost:8080/registro", {
      method: "POST",

      headers: {
        "Content-Type": "application/json",
      },

      body: JSON.stringify({
        nombre,
        email,
        password,
      }),
    });

    const data = await response.json();

    console.log(data);

    if (response.ok) {
      // GUARDAR ID

      localStorage.setItem("id_usuario", data.id);

      alert("Cuenta creada correctamente");

      // IR A CREAR MASCOTA

      window.location.href = "mascota.html";
    } else {
      alert("Error al registrarse");
    }
  } catch (error) {
    console.log(error);

    alert("Error de conexión");
  }
}
