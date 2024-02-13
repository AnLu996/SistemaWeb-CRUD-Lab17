using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;


using Lab17_A.Datos;
using Lab17_A.Models;

namespace Lab17_A.Controllers
{
    public class MantenedorController : Controller
    {
        Conexion conexion = new Conexion();

        VacunaDatos _VacunaDatos = new VacunaDatos();

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
                var respuesta = _VacunaDatos.Registrar(oUsuario.perfil, oUsuario.contraseña);

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
            var respuesta = _VacunaDatos.Login(oUsuario.perfil, oUsuario.contraseña);

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
            //LA VISTA MOSTRARÁ UNA LISTA
            
            var oLista = _VacunaDatos.Listar();

            return View(oLista);
        }

        [HttpGet]
        public IActionResult Guardar()
        {
            //MÉTODO SOLO DEVUELVE LA VISTA

            return View();
        }

        [HttpPost]
        public IActionResult Guardar(VacunaModel oVacuna)
        {
            //MÉTODO RECIBE EL OBJETO PARA GUARDARLO EN BD
            if(!ModelState.IsValid)
                return View();

            var respuesta = _VacunaDatos.Guardar(oVacuna);

            if (respuesta)
                return RedirectToAction("Listar");
            else 
                return View();
        }

        [HttpGet]
        public IActionResult Editar(int IdVacuna)
        {
            //MÉTODO SOLO DEVUELVE LA VISTA

            var oVacuna = _VacunaDatos.Obtener(IdVacuna);

            return View(oVacuna);
        }

        [HttpPost]
        public IActionResult Editar(VacunaModel oVacuna)
        {
            //MÉTODO RECIBE EL OBJETO PARA GUARDARLO EN BD
            if (!ModelState.IsValid)
                return View();

            var respuesta = _VacunaDatos.Editar(oVacuna);

            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }

        [HttpGet]
        public IActionResult Eliminar(int IdVacuna)
        {
            //MÉTODO SOLO DEVUELVE LA VISTA

            var oVacuna = _VacunaDatos.Obtener(IdVacuna);

            return View(oVacuna);
        }

        [HttpPost]
        public IActionResult Eliminar(VacunaModel oVacuna)
        {
            //MÉTODO RECIBE EL OBJETO PARA GUARDARLO EN BD

            var respuesta = _VacunaDatos.Eliminar(oVacuna.IdVacuna);

            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }

    }
}
