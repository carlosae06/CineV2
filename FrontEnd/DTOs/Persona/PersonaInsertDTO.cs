using System.ComponentModel.DataAnnotations;

namespace FrontEnd.DTOs.Persona
{
    public class PersonaInsertDTO
    {
        [Required(ErrorMessage = "El campo CI es obligatorio.")]
        [StringLength(20)]
        public string Ci { get; set; } = null!;
        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        [StringLength(50)]
        public string Nombre { get; set; } = null!;
        [Required(ErrorMessage = "El campo Apellido Paterno es obligatorio.")]
        [StringLength(20)]
        public string ApellidoPaterno { get; set; } = null!;
        [StringLength(20)]
        [Required(ErrorMessage = "El campo Apellido Paterno es obligatorio.")]
        public string ApellidoMaterno { get; set; } = null!;
        [StringLength(10)]
        public string? Telefono { get; set; }
    }
    public class PersonaActualizarDTO : PersonaInsertDTO{

        [Required(ErrorMessage = "El campo nombre es obligatorio.")]
        [StringLength(50)]
        public string Nombre { get; set; } = null!;

    }
}
