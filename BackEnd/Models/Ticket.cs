using System;
using System.Collections.Generic;

namespace BackEnd.Models
{
    public partial class Ticket
    {
        public Ticket()
        {
            Controls = new HashSet<Control>();
        }

        public int Id { get; set; }
        public string Cod { get; set; } = null!;
        public decimal Precio { get; set; }
        public string TipoPago { get; set; } = null!;
        public int IdButaca { get; set; }
        public int IdCliente { get; set; }
        public int? IdUsuario { get; set; }
        public int IdFunsion { get; set; }
        public int IdVenta { get; set; }

        public virtual Butaca IdButacaNavigation { get; set; } = null!;
        public virtual Cliente IdClienteNavigation { get; set; } = null!;
        public virtual Funsion IdFunsionNavigation { get; set; } = null!;
        public virtual Usuario? IdUsuarioNavigation { get; set; }
        public virtual Vent IdVentaNavigation { get; set; } = null!;
        public virtual ICollection<Control> Controls { get; set; }
    }
}
