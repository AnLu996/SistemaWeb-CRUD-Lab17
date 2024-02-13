using Lab17_A.Datos;
using Lab17_A.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Lab17_A.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        Conexion conexion = new Conexion();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /*
        public IActionResult CerrarSesion()
        {
            conexion.RestaurarCadenaConexion();
            HttpContext.Session.SetString("Usuario", null);
            return RedirectToAction("Login");
        }*/
    }
}