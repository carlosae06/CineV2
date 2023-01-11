using System;
using System.Collections.Generic;

namespace BackEnd.Models
{
    public partial class Butaca
    {
        public Butaca()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public bool? Estado { get; set; }
        public int IdSeccion { get; set; }

        public virtual Seccion IdSeccionNavigation { get; set; } = null!;
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
