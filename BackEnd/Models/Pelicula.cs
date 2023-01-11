using System;
using System.Collections.Generic;

namespace BackEnd.Models
{
    public partial class Pelicula
    {
        public Pelicula()
        {
            Funsions = new HashSet<Funsion>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Clasificacion { get; set; } = null!;
        public DateTime FechaEstreno { get; set; }
        public string Protagonista { get; set; } = null!;
        public string Director { get; set; } = null!;
        public string Idioma { get; set; } = null!;
        public string Duracion { get; set; } = null!;
        public string? Sinopsis { get; set; }
        public bool? Estado { get; set; }
        public int IdCategoria { get; set; }

        public virtual Categori IdCategoriaNavigation { get; set; } = null!;
        public virtual ICollection<Funsion> Funsions { get; set; }
    }
}
