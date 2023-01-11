using System.ComponentModel.DataAnnotations;

namespace BackEnd.DTOs.Sala
{
    public class SalaUpdateDTO
    {
        [Required]
        [StringLength(10)]
        public string Cod { get; set; } = null!;
        [Required]
        public int Fila { get; set; }
        [Required]
        public int Columna { get; set; }
        [Required]
        public int Capacidad { get; set; }

        public bool? Estado { get; set; }
    }
}
