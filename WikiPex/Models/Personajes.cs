using System;
using System.Collections.Generic;

namespace WikiPex.Models
{
    public partial class Personajes
    {
        public string Nombre { get; set; } = null!;
        public string Frase { get; set; } = null!;
        public string Seudonimo { get; set; } = null!;
        public string Historia { get; set; } = null!;
        public string Nombrereal { get; set; } = null!;
        public string Edad { get; set; } = null!;
        public string Planetanatal { get; set; } = null!;
        public string Url { get; set; } = null!;
        public int Id { get; set; }
        public int? IdHabilidad { get; set; }

        public virtual Habilidades? IdHabilidadNavigation { get; set; }
    }
}
