using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WikiPex.Areas.Administrador.Models;
using WikiPex.Models;

namespace WikiPex.Areas.Administrador.Controllers
{
    [Authorize(Roles = "Administrador")]
    [Area("Administrador")]
    public class HabilidadesController : Controller
    {
        private readonly sistem21_wikiapexContext context;
        private readonly IWebHostEnvironment em;

        public HabilidadesController(sistem21_wikiapexContext context, IWebHostEnvironment env)
        {
            this.context = context;
            this.em = env;
        }
        public IActionResult Index(IndexViewModel vm)
        {
            vm.Habilidades = context.Habilidades.OrderBy(x=>x.IdHabilidades);
            return View(vm);
        }
        public IActionResult Agregar()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Agregar(HabilidadesViewModel vm)
        {
            if (vm != null)
            {
                byte contador = 1;
                if (string.IsNullOrWhiteSpace(vm.NombreDefinitiva))
                    ModelState.AddModelError("", "Escribe el nombre de las habilidades");
                if (ModelState.IsValid)
                {
                    var habilidad = new Habilidades
                    {
                        NombreTactica = vm.NombreTactica,
                        DescripcionTactica = vm.DescripcionTactica,
                        NombrePasiva = vm.NombrePasiva,
                        DescripcionPasiva = vm.DescripcionPasiva,
                        NombreDefinitiva = vm.NombreDefinitiva,
                        DescripcionDefinitiva = vm.DescripcionDefinitiva
                    };
                    context.Add(habilidad);
                    context.SaveChanges();
                    if (vm.ImagenTactica != null && vm.ImagenPasiva != null && vm.ImagenDefinitiva != null)
                    {
                        for (int i = 1; i < 4; i++)
                        {
                            string nuevaimg = em.WebRootPath + $"/Imagenes/Habilidades/{vm.PersonajeNombre?.ToString()?.ToLower()}h{i}.png";
                            var archivo = System.IO.File.Create(nuevaimg);
                            if (contador == 1)
                                vm.ImagenTactica.CopyTo(archivo);
                            else if (contador == 2)
                                vm.ImagenPasiva.CopyTo(archivo);
                            else
                                vm.ImagenDefinitiva.CopyTo(archivo);
                            archivo.Close();
                            contador++;
                        }
                        //string nuevaimg = em.WebRootPath + $"/Imagenes/Habilidades/{vm.PersonajesNombre?.ToString()?.ToLower()}.png";
                        //var archivo = System.IO.File.Create(nuevaimg);
                        //vm.ImagenTactica.CopyTo(archivo);
                        //archivo.Close();
                    }
                    return RedirectToAction("Index");
                }
            }
            return View(vm);
        }
        public IActionResult Editar(int id)
        {
            var habilidad = context.Habilidades.Find(id);
            if (habilidad == null)
                return RedirectToAction("Index");
            return View(habilidad);
        }
        [HttpPost]
        public IActionResult Editar(Habilidades h)
        {
            if (string.IsNullOrWhiteSpace(h.NombreTactica) || string.IsNullOrWhiteSpace(h.NombrePasiva) || string.IsNullOrWhiteSpace(h.NombreDefinitiva))
                ModelState.AddModelError("", "Favor de escribir el nombre de las habilidades");
            if (context.Habilidades.Any(x => (x.NombreTactica == h.NombreTactica || x.NombrePasiva == h.NombrePasiva ||
            x.NombreDefinitiva == h.NombreDefinitiva) && x.IdHabilidades != h.IdHabilidades))
                ModelState.AddModelError("", "El nombre de una habilidad ya existe");
            if (ModelState.IsValid)
            {
                var habilidad = context.Habilidades.Find(h.IdHabilidades);
                if (habilidad == null)
                    return RedirectToAction("Index");
                habilidad.NombreTactica = h.NombreTactica;
                habilidad.DescripcionTactica = h.DescripcionTactica;
                habilidad.NombrePasiva= h.NombrePasiva;
                habilidad.DescripcionPasiva = h.DescripcionPasiva;
                habilidad.NombreDefinitiva = h.NombreDefinitiva;
                habilidad.DescripcionDefinitiva=h.DescripcionDefinitiva;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
                return View(h);
        }
        public IActionResult Eliminar(int id)
        {
            var habilidades = context.Habilidades.Find(id);
            if (habilidades == null)
                return RedirectToAction("Index");
            return View(habilidades);
        }
        [HttpPost]
        public IActionResult Eliminar(Habilidades habilidad)
        {
            var Habilidades = context.Habilidades.Find(habilidad.IdHabilidades);
            if (Habilidades == null)
                ModelState.AddModelError("", "La habilidad no existe o ya ha sido eliminada por alguien más");
            else
            {
                if (context.Personajes.Any(x => x.IdHabilidad == habilidad.IdHabilidades))
                    ModelState.AddModelError("", "La habilidad no se puede eliminar debido a que tiene un personaje dado de alta con tales habilidades");
                if (ModelState.IsValid)
                {
                    context.Remove(Habilidades);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(Habilidades);
        }
    }
}