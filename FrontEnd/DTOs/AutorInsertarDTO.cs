using System.ComponentModel.DataAnnotations;

namespace FrontEnd.DTOs
{
    public class AutorInsertarDTO
    {

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; } = null!;
        [Required]
        [StringLength(50)]
        public string Documento { get; set; } = null!;
        [Required]
        public DateTime FechaNacimiento { get; set; }
        [Required]
        [StringLength(300)]
        [Url]
        public string UrlBiografia { get; set; } = null!;
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Correo { get; set; } = null!;
        public string? Naciolidad { get; set; }
        
    }

    public class AutorActualizarDTO : AutorInsertarDTO
    {
        [Required]
        public bool Estado { get; set; }


    }
}
