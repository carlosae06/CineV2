using System.ComponentModel.DataAnnotations;

namespace FrontEnd.DTOs.Categoria
{
    public class CategoriaDTO
    {
    
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo nombre es obligatorio.")]
        [StringLength(50)]
        public string Nombre { get; set; }

        public bool Estado { get; set; }
    }
}