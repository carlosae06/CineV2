using System.ComponentModel.DataAnnotations;

namespace FrontEnd.DTOs
{
    public class UsuarioLoginDTO
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty ;

    }
}
