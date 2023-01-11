using System;
using System.Collections.Generic;

namespace BackEnd.Models
{
    public partial class Control
    {
        public int Id { get; set; }
        public int IdTicket { get; set; }
        public bool? Estado { get; set; }

        public virtual Ticket IdTicketNavigation { get; set; } = null!;
    }
}
