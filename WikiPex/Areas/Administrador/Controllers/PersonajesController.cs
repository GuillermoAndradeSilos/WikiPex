using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using WikiPex.Areas.Administrador.Models;
using WikiPex.Models;

namespace WikiPex.Areas.Administrador.Controllers
{
    [Authorize(Roles = "Administrador")]
    [Area("Administrador")]
    public class PersonajesController : Controller
    {
        private readonly sistem21_wikiapexContext context;
        private readonly IWebHostEnvironment em;

        public PersonajesController(sistem21_wikiapexContext context, IWebHostEnvironment env)
        {
            this.context = context;
            this.em = env;
        }
        public IActionResult Index(IndexViewModel vm)
        {
            vm.Personajes = context.Personajes.Select(x => new Personajes { Nombre = x.Nombre, Seudonimo = x.Seudonimo,Id=x.Id }).OrderBy(x => x.Nombre);
            return View(vm);
        }
        public IActionResult Agregar()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Agregar(PersonajesViewModel vm)
        {
            if (vm.Personajes != null)
            {
                if (string.IsNullOrWhiteSpace(vm.Personajes.Nombre))
                    ModelState.AddModelError("", "Escribe el nombre del producto");
                if (ModelState.IsValid)
                {
                    context.Add(vm.Personajes);
                    context.SaveChanges();
                    if (vm.ImagenPersonaje == null)
                    {
                        string nodisp = em.WebRootPath + "/Imagenes/Personajes/no-disponible.png";
                        string nuevaimg = em.WebRootPath + $"/Imagenes/Personajes/{vm.Personajes.Nombre.ToLower()}.png";
                        System.IO.File.Copy(nodisp, nuevaimg, true);
                    }
                    else
                    {
                        string nuevaimg = em.WebRootPath + $"/Imagenes/Personajes/{vm.Personajes.Nombre.ToLower()}.png";
                        var archivo = System.IO.File.Create(nuevaimg);
                        vm.ImagenPersonaje.CopyTo(archivo);
                        archivo.Close();
                    }
                    if (vm.ImagenLore == null)
                    {
                        string nodisp = em.WebRootPath + "/Imagenes/Personajes/no-disponible.png";
                        string nuevaimg = em.WebRootPath + $"/Imagenes/Habilidades/{vm.Personajes.Nombre.ToLower()}lore.jpg";
                        System.IO.File.Copy(nodisp, nuevaimg, true);
                    }
                    else
                    {
                        string nuevaimg = em.WebRootPath + $"/Imagenes/Habilidades/{vm.Personajes.Nombre.ToLower()}lore.jpg";
                        var archivo = System.IO.File.Create(nuevaimg);
                        vm.ImagenLore.CopyTo(archivo);
                        archivo.Close();
                    }
                    return RedirectToAction("Index");
                }
            }
            return View(vm);
        }
        public IActionResult Editar(int id)
        {
            var p = context.Personajes.Find(id);
            if (p == null)
                return RedirectToAction("Index");
            PersonajesViewModel vm = new PersonajesViewModel();
            vm.Personajes = p;
            return View(vm);
        }
        [HttpPost]
        public IActionResult Editar(PersonajesViewModel vm)
        {
            if (vm.Personajes != null)
            {
                var p = context.Personajes.Find(vm.Personajes.Id);
                if (p == null)
                    return RedirectToAction("Index");
                if (string.IsNullOrWhiteSpace(vm.Personajes.Nombre))
                    ModelState.AddModelError("", "Escriba el nombre del personaje.");
                if (ModelState.IsValid)
                {
                    p.Nombre = vm.Personajes.Nombre;
                    p.Url = vm.Personajes.Url;
                    p.Nombrereal = vm.Personajes.Nombrereal;
                    p.IdHabilidad = vm.Personajes.IdHabilidad;
                    p.Planetanatal=vm.Personajes.Planetanatal;
                    p.Edad = vm.Personajes.Edad;
                    p.Frase = vm.Personajes.Frase;
                    p.Seudonimo = vm.Personajes.Seudonimo;
                    p.Historia = vm.Personajes.Historia;
                    context.SaveChanges();
                }
                if (vm.ImagenPersonaje != null)
                {
                    string ruta = em.WebRootPath + $"/imagenes/personajes/{vm.Personajes.Nombre.ToLower()}.png";
                    var archivo = System.IO.File.Create(ruta);
                    vm.ImagenPersonaje.CopyTo(archivo);
                    archivo.Close();
                }
                if(vm.ImagenLore != null)
                {
                    var ruta = em.WebRootPath + $"/imagenes/habilidades/{vm.Personajes.Nombre.ToLower()}lore.jpg";
                    var archivo = System.IO.File.Create(ruta);
                    vm.ImagenLore.CopyTo(archivo);
                    archivo.Close();
                }
                return RedirectToAction("Index");
            }
            return View(vm);
        }
        public IActionResult Eliminar(int id)
        {
            var personaje = context.Personajes.Find(id);
            if (personaje == null)
                return RedirectToAction("Index");
            return View(personaje);
        }
        [HttpPost]
        public IActionResult Eliminar(Personajes p)
        {
            var personaje = context.Personajes.Find(p.Id);
            if (personaje == null)
                ModelState.AddModelError("", "El personaje no existe o ya ha sido eliminado por alguien más.");
            else
            {
                string ruta = em.WebRootPath + $"/imagenes/habilidades/{personaje.Nombre.ToLower()}lore.jpg";
                System.IO.File.Delete(ruta);
                ruta = em.WebRootPath + $"/imagenes/personajes/{personaje.Nombre.ToLower()}.png";
                System.IO.File.Delete(ruta);
                context.Remove(personaje);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(p);
        }
    }
}
