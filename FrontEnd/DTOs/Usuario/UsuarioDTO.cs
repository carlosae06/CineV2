using System.ComponentModel.DataAnnotations;

namespace FrontEnd.DTOs.Usuario
{
    public class UsuarioDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string UserName { get; set; } = null!;
        [Required]
        [StringLength(400)]
        public string Password { get; set; } = null!;
        [StringLength(200)]
        public string? Correo { get; set; }

        public bool Estado { get; set; }
        [Required]
        public int Idpersona { get; set; }
        [Required]
        public int IdRol { get; set; }
        public int RolId { get; set; }
        public string RolCod { get; set; } = string.Empty!;
        public string RolNombre { get; set; } = string.Empty!;

        public string PersonaNombre { get; set; }

    }
}
