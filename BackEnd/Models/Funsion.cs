using System;
using System.Collections.Generic;

namespace BackEnd.Models
{
    public partial class Funsion
    {
        public Funsion()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Horario { get; set; } = null!;
        public int IdSala { get; set; }
        public int IdPelicula { get; set; }

        public virtual Pelicula IdPeliculaNavigation { get; set; } = null!;
        public virtual Sala IdSalaNavigation { get; set; } = null!;
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
