using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Parte02.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Parte02.Controllers
{
    public class HomeController : Controller
    {
        IUsuariosRepository _usuariosRepository;
        public HomeController(IUsuariosRepository usuariosRepository)
        {
            _usuariosRepository = usuariosRepository;
        }
        [SesionTerminada]
        public IActionResult Index()
        {
            Usuario en = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("Usuario"));
            return View("Index", en);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Loguear([Bind("Usuario,Contrasenia")] Login login)
        {
            Usuario en = _usuariosRepository.login(login.Usuario, login.Contrasenia);
            if (en == null)
            {
                login.Mensaje = "Usuasrio o contraseña incorrecta.";
                return View("Login", login);
            }
            HttpContext.Session.SetString("Usuario", JsonConvert.SerializeObject(en));
            return Redirect("~/Home/Index");
        }
        public IActionResult Deslogueo()
        {
            HttpContext.Session.SetString("Usuario", "");
            return Redirect("~/");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
