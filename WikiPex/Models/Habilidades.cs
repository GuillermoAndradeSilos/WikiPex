using System;
using System.Collections.Generic;

namespace WikiPex.Models
{
    public partial class Habilidades
    {
        public Habilidades()
        {
            Personajes = new HashSet<Personajes>();
        }

        public int IdHabilidades { get; set; }
        public string NombreTactica { get; set; } = null!;
        public string DescripcionTactica { get; set; } = null!;
        public string NombrePasiva { get; set; } = null!;
        public string DescripcionPasiva { get; set; } = null!;
        public string NombreDefinitiva { get; set; } = null!;
        public string DescripcionDefinitiva { get; set; } = null!;

        public virtual ICollection<Personajes> Personajes { get; set; }
    }
}
