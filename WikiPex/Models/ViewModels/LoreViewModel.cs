namespace WikiPex.Models.ViewModels
{
    public class LoreViewModel
    {
        public IEnumerable<Personajes>? Personaje { get; set; }
        public IEnumerable<Habilidades>? PersonajeHabilidades { get; set; }
        public IEnumerable<Personajes>? PersonajesSiguientes { get; set; }
    }
}
