using System.ComponentModel.DataAnnotations;

namespace BackEnd.DTOs.Usuario
{
    public class UsuarioUpdateDTO
    {
        [Required]
        [StringLength(50)]
        public string UserName { get; set; } = null!;
        [Required]
        [StringLength(400)]
        public string Password { get; set; } = null!;
        [StringLength(200)]
        public string? Correo { get; set; }

        public bool? Estado { get; set; }
        [Required]
        public int Idpersona { get; set; }
        [Required]
        public int IdRol { get; set; }
    }
}
