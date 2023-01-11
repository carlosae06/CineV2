using System;
using System.Collections.Generic;

namespace BackEnd.Models
{
    public partial class Categori
    {
        public Categori()
        {
            Peliculas = new HashSet<Pelicula>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public bool? Estado { get; set; }

        public virtual ICollection<Pelicula> Peliculas { get; set; }
    }
}
