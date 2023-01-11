using System.ComponentModel.DataAnnotations;

namespace FrontEnd.DTOs.Rol
{
    public class RolDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(10)]
        public string Cod { get; set; } = null!;
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; } = null!;

        public bool Estado { get; set; }
    }
}
