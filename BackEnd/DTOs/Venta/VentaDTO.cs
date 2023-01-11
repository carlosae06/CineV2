using System.ComponentModel.DataAnnotations;

namespace BackEnd.DTOs.Venta
{
    public class VentaDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        public decimal PrecioTotal { get; set; }
        [StringLength(10)]
        public string? Documento { get; set; }
        [StringLength(50)]
        public string? NumDocumento { get; set; }
        [StringLength(10)]
        public string? Complemento { get; set; }
        [Required]
        [StringLength(50)]
        public string Telefono { get; set; } = null!;
    }
}