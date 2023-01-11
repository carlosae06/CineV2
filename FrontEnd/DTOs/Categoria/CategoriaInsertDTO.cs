using System.ComponentModel.DataAnnotations;

namespace FrontEnd.DTOs.Categoria
{
    public class CategoriaInsertDTO
    {
        [Required(ErrorMessage = "El campo nombre es obligatorio.")]
        [StringLength(50)]
        public string? Nombre { get; set; }

 
    }

    public class CategoriaActualizarDTO : CategoriaInsertDTO {
       
        [Required]
        public bool Estado { get; set; }
    }
}
