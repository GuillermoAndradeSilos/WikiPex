using WikiPex.Models;

namespace WikiPex.Areas.Administrador.Models
{
    public class PersonajesViewModel
    {
        public Personajes? Personajes { get; set; }
        public IFormFile? ImagenPersonaje { get; set; }
        public IFormFile? ImagenLore { get; set; }
    }
}
