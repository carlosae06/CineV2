using System;
using System.Collections.Generic;

namespace BackEnd.Models
{
    public partial class Seccion
    {
        public Seccion()
        {
            Butacas = new HashSet<Butaca>();
        }

        public int Id { get; set; }
        public string Cod { get; set; } = null!;
        public bool? Estado { get; set; }
        public int IdSala { get; set; }

        public virtual Sala IdSalaNavigation { get; set; } = null!;
        public virtual ICollection<Butaca> Butacas { get; set; }
    }
}
