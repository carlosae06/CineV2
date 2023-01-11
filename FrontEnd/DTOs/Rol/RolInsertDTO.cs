using System.ComponentModel.DataAnnotations;

namespace FrontEnd.DTOs.Rol
{
    public class RolInsertDTO
    {
        [Required(ErrorMessage = "El campo COD es obligatorio.")]
        [StringLength(10)]
        public string Cod { get; set; } = null!;
        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        [StringLength(50)]
        public string Nombre { get; set; } = null!;

        public bool Estado { get; set; }
    }
    public class RolActualizarDTO : RolInsertDTO {

        public bool Estado { get; set; }
    }
}
