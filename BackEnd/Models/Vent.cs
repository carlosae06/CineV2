using System;
using System.Collections.Generic;

namespace BackEnd.Models
{
    public partial class Vent
    {
        public Vent()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal PrecioTotal { get; set; }
        public string? Documento { get; set; }
        public string? NumDocumento { get; set; }
        public string? Complemento { get; set; }
        public string Telefono { get; set; } = null!;

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
