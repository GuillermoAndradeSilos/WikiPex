using WikiPex.Models;

namespace WikiPex.Areas.Administrador.Models
{
    public class HabilidadesViewModel
    {
        public string? PersonajeNombre { get; set; }
        public IFormFile? ImagenTactica { get; set; }
        public IFormFile? ImagenPasiva { get; set; }
        public IFormFile? ImagenDefinitiva { get; set; }
        public string? NombreTactica { get; set; }
        public string? DescripcionTactica { get; set; }
        public string? NombrePasiva { get; set; }
        public string? DescripcionPasiva { get; set; }
        public string? NombreDefinitiva{ get; set; }
        public string? DescripcionDefinitiva { get; set; }
    }
}
