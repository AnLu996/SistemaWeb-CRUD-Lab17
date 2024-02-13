using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;


using Lab17_A.Datos;
using Lab17_A.Models;

namespace Lab17_A.Controllers
{
    public class MantenedorController : Controller
    {
        Conexion conexion = new Conexion();

        PeliculaDatos _PeliculaDatos = new PeliculaDatos();

        [HttpGet]
        public ActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrar(Usuario oUsuario)
        {
            if(oUsuario.contraseña != oUsuario.ConfirmarContraseña)
            {
                ViewData["Mensaje"] = "Las contraseñas no coinciden";
                return View();
            }
            else
            {
                var respuesta = _PeliculaDatos.Registrar(oUsuario.perfil, oUsuario.contraseña);

                if (respuesta)
                {
                    ViewData["Mensaje"] = "Registro exitoso";
                    return RedirectToAction("Login");
                }
                else
                    ViewData["Mensaje"] = "Ocurrió un error :/";
                    return View();
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuario oUsuario)
        {
            var respuesta = _PeliculaDatos.Login(oUsuario.perfil, oUsuario.contraseña);

            if (respuesta == 1)
            {
                // Cambiar la cadena de conexión
                conexion.CambiarCadenaConexion(oUsuario.perfil.Trim(), oUsuario.contraseña.Trim());
                // Almacenar el usuario en la sesión
                //HttpContext.Session.SetString("Usuario", oUsuario.perfil);
                return RedirectToAction("Listar","Mantenedor");
            }
            else if (respuesta == 2)
            {
                ViewData["Mensaje"] = "Contraseña incorrecta";
                return View();
            }
            ViewData["Mensaje"] = "Ocurrió un error inesperado :/";
            return View();
        }

        [HttpGet]
        public IActionResult Listar()
        {
            //LA VISTA MOSTRARÁ UNA LISTA DE PELICULAS
            
            var oLista = _PeliculaDatos.Listar();

            return View(oLista);
        }

        [HttpGet]
        public IActionResult Guardar()
        {
            //MÉTODO SOLO DEVUELVE LA VISTA

            return View();
        }

        [HttpPost]
        public IActionResult Guardar(PeliculaModel oPelicula)
        {
            //MÉTODO RECIBE EL OBJETO PARA GUARDARLO EN BD
            if(!ModelState.IsValid)
                return View();

            var respuesta = _PeliculaDatos.Guardar(oPelicula);

            if (respuesta)
                return RedirectToAction("Listar");
            else 
                return View();
        }

        [HttpGet]
        public IActionResult Editar(int IdPelicula)
        {
            //MÉTODO SOLO DEVUELVE LA VISTA

            var oPelicula = _PeliculaDatos.Obtener(IdPelicula);

            return View(oPelicula);
        }

        [HttpPost]
        public IActionResult Editar(PeliculaModel oPelicula)
        {
            //MÉTODO RECIBE EL OBJETO PARA GUARDARLO EN BD
            if (!ModelState.IsValid)
                return View();

            var respuesta = _PeliculaDatos.Editar(oPelicula);

            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }

        [HttpGet]
        public IActionResult Eliminar(int IdPelicula)
        {
            //MÉTODO SOLO DEVUELVE LA VISTA

            var oPelicula = _PeliculaDatos.Obtener(IdPelicula);

            return View(oPelicula);
        }

        [HttpPost]
        public IActionResult Eliminar(PeliculaModel oPelicula)
        {
            //MÉTODO RECIBE EL OBJETO PARA GUARDARLO EN BD

            var respuesta = _PeliculaDatos.Eliminar(oPelicula.IdPelicula);

            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }

    }
}
