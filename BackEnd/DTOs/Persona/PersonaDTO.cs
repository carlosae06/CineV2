using System.ComponentModel.DataAnnotations;

namespace BackEnd.DTOs.Persona
{
    public class PersonaDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Ci { get; set; } = null!;
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; } = null!;
        [Required]
        [StringLength(20)]
        public string ApellidoPaterno { get; set; } = null!;
        [StringLength(20)]
        [Required]
        public string ApellidoMaterno { get; set; } = null!;
        [StringLength(10)]
        public string? Telefono { get; set; }

    }
}
