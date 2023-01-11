using System.ComponentModel.DataAnnotations;

namespace FrontEnd.DTOs.Sala
{
    public class SalaDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(10)]
        public string Cod { get; set; } = null!;
        [Required]
        public int Fila { get; set; }
        [Required]
        public int Columna { get; set; }
        [Required]
        public int Capacidad { get; set; }

        public bool Estado { get; set; }
    }
}
