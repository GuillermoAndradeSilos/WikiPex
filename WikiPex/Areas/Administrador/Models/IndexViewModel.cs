using WikiPex.Models;

namespace WikiPex.Areas.Administrador.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Personajes>? Personajes { get; set; }
        public IEnumerable<Habilidades>? Habilidades { get; set; }
    }
}
