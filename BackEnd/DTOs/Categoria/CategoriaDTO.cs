using System.ComponentModel.DataAnnotations;

namespace BackEnd.DTOs.Categoria
{
    public class CategoriaDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string? Nombre { get; set; }

        public bool Estado { get; set; }
    }
}