using System.ComponentModel.DataAnnotations;

namespace FrontEnd.DTOs.Seccion
{
    public class SeccionInsertDTO
    {
        [Required(ErrorMessage = "El campo Cod es obligatorio.")]
        [StringLength(50)]
        public string Cod { get; set; } = null!;
        [Required(ErrorMessage = "El campo Sala es obligatorio.")]
        public int IdSala { get; set; }

    }
    public class SeccionActualizarDTO : SeccionInsertDTO
    {
        [Required]
        public bool Estado { get; set; }


    }
}
