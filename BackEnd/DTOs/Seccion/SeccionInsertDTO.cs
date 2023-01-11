using System.ComponentModel.DataAnnotations;

namespace BackEnd.DTOs.Seccion
{
    public class SeccionInsertDTO
    {
        [Required]
        [StringLength(10)]
        public string Cod { get; set; } = null!;

        public bool? Estado { get; set; }
        [Required]
        public int IdSala { get; set; }
    }
}
