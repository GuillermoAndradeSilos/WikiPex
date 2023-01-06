using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WikiPex.Models;
using WikiPex.Models.ViewModels;

namespace WikiPex.Controllers
{
    public class HomeController : Controller
    {
        private readonly sistem21_wikiapexContext context;

        public HomeController(sistem21_wikiapexContext context)
        {
            this.context = context;
            
        }
        [Route("/")]
        [Route("/PaginaPrincipal")]
        [Route("/Home")]
        public IActionResult Index()
        {
            ViewBag.Estilos = "Estilos.css";
            return View();
        }
        [Route("/Personajes")]
        public IActionResult Personajes()
        {
            ViewBag.Estilos = "EstiloPersonajes.css";
            var datos = new PersonajesViewModel
            {
                Personajes = context.Personajes.Select(x => new Personajes
                {
                    Nombre = x.Nombre,
                    Seudonimo = x.Seudonimo
                }).OrderBy(x=>x.Nombre)
            };
            //var datos= context.Personajes.Select(x => new Personajes
            //{
            //    Nombre = x.Nombre,
            //    Seudonimo = x.Seudonimo,
            //    Id=x.Id
            //});
            return View(datos);
        }
        [Route("/L/{id}")]
        public IActionResult Lore(string? id)
        {
            ViewBag.Estilos = "EstiloLore.css";
            if (id == null)
                return RedirectToAction("Index");
            Personajes? Personaje = (Personajes?)context.Personajes.Where(x=>x.Nombre==id).FirstOrDefault();
            var datos = new LoreViewModel
            {
                Personaje = context.Personajes.Include(x=>x.IdHabilidadNavigation).Where(x => x.Nombre == id).Select(x => new Personajes
                {
                    Edad = x.Edad,
                    Frase = x.Frase,
                    Nombre = x.Nombre,
                    Historia = x.Historia,
                    Nombrereal = x.Nombrereal,
                    Seudonimo = x.Seudonimo,
                    Url = x.Url,
                    Planetanatal = x.Planetanatal,
                    IdHabilidadNavigation = x.IdHabilidadNavigation
                }),
                PersonajesSiguientes=context.Personajes.Where(x=>x.Nombre!=id).Select(x => new Personajes
                {
                    Nombre = x.Nombre,
                    Seudonimo = x.Seudonimo
                }).OrderBy(x=>x.Nombre).Take(3),
                PersonajeHabilidades = from cx in context.Habilidades
                                       where cx.IdHabilidades == Personaje.IdHabilidad
                                       select new Habilidades { NombreTactica=cx.NombreTactica,DescripcionTactica=cx.DescripcionTactica,
                                       NombrePasiva=cx.NombrePasiva,DescripcionPasiva=cx.DescripcionPasiva,NombreDefinitiva=cx.NombreDefinitiva,DescripcionDefinitiva=cx.DescripcionDefinitiva}
            };
            return View(datos);
        }
        [Route("/Mapas")]
        public IActionResult Mapas()
        {
            ViewBag.Estilos = "EstiloMapas.css";
            return View();
        }
        public IActionResult IniciarSesion()
        {
            return View();
        }
        [HttpPost]
        public IActionResult IniciarSesion(Login login)
        {
            if (login.UserName.ToLower() == "prueba" && login.Password == "Admina1")
            {
                var listaclaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name,"Guillermo Saúl Andrade Silos"),
                    new Claim(ClaimTypes.Role,"Administrador")
                };
                var identidad = new ClaimsIdentity(listaclaims, CookieAuthenticationDefaults.AuthenticationScheme);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identidad),
                    new AuthenticationProperties()
                    {
                        ExpiresUtc = DateTimeOffset.Now.AddMinutes(5),
                        IsPersistent = true
                    });
                return RedirectToAction("Index", "Home", new { Area = "Administrador" });
            }
            {
                ModelState.AddModelError("", "Nombre de usuario o contraseña incorrecta");
                return View(login);
            }
        }
        public IActionResult CerrarSesion()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
