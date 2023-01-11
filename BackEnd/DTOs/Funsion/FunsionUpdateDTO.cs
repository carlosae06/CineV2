using System.ComponentModel.DataAnnotations;

namespace BackEnd.DTOs.Funsion
{
    public class FunsionUpdateDTO
    {
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        [StringLength(30)]
        public string Horario { get; set; } = null!;
        [Required]
        public int IdSala { get; set; }
        [Required]
        public int IdPelicula { get; set; }
    }
}
