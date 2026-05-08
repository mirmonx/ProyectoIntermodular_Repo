console.log("LOGIN JS CARGADO");
async function login() {
  const email = document.getElementById("email").value;

  const password = document.getElementById("password").value;

  const response = await fetch("http://localhost:8080/login", {
    method: "POST",

    headers: {
      "Content-Type": "application/json",
    },

    body: JSON.stringify({
      email,
      password,
    }),
  });

  const data = await response.json();

  console.log(data);

  if (response.ok) {
    localStorage.setItem("id_usuario", data.id);

    window.location.href = "perfil.html";
  } else {
    alert("Login incorrecto");
  }
}
