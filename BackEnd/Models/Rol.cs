using System;
using System.Collections.Generic;

namespace BackEnd.Models
{
    public partial class Rol
    {
        public Rol()
        {
            Usuarios = new HashSet<Usuario>();
        }

        public int Id { get; set; }
        public string Cod { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public bool? Estado { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
