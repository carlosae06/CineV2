using System;
using System.Collections.Generic;

namespace BackEnd.Models
{
    public partial class Persona
    {
        public Persona()
        {
            Clientes = new HashSet<Cliente>();
            Usuarios = new HashSet<Usuario>();
        }

        public int Id { get; set; }
        public string Ci { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string ApellidoPaterno { get; set; } = null!;
        public string ApellidoMaterno { get; set; } = null!;
        public string? Telefono { get; set; }

        public virtual ICollection<Cliente> Clientes { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
