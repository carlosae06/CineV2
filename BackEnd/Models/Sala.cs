using System;
using System.Collections.Generic;

namespace BackEnd.Models
{
    public partial class Sala
    {
        public Sala()
        {
            Funsions = new HashSet<Funsion>();
            Seccions = new HashSet<Seccion>();
        }

        public int Id { get; set; }
        public string Cod { get; set; } = null!;
        public int Fila { get; set; }
        public int Columna { get; set; }
        public int Capacidad { get; set; }
        public bool? Estado { get; set; }

        public virtual ICollection<Funsion> Funsions { get; set; }
        public virtual ICollection<Seccion> Seccions { get; set; }
    }
}
