using System;
using System.Collections.Generic;

namespace BackEnd.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Correo { get; set; }
        public bool Estado { get; set; }
        public int Idpersona { get; set; }
        public int IdRol { get; set; }

        public virtual Rol IdRolNavigation { get; set; } = null!;
        public virtual Persona IdpersonaNavigation { get; set; } = null!;
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
