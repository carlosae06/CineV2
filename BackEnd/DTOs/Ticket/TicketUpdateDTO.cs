using System.ComponentModel.DataAnnotations;

namespace BackEnd.DTOs.Ticket
{
    public class TicketUpdateDTO
    {
        [Required]
        [StringLength(50)]
        public string Cod { get; set; } = null!;
        [Required]
        public decimal Precio { get; set; }
        [Required]
        [StringLength(50)]
        public string TipoPago { get; set; } = null!;
        [Required]
        public int IdButaca { get; set; }
        [Required]
        public int IdCliente { get; set; }
        [Required]
        public int IdFunsion { get; set; }
        [Required]
        public int IdVenta { get; set; }

        public int? IdUsuario { get; set; }
    }
}
