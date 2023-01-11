using System.ComponentModel.DataAnnotations;

namespace BackEnd.DTOs.Usuario
{
    public class UsuarioLoginDTO
    {

        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
