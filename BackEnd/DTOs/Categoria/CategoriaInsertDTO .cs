using System.ComponentModel.DataAnnotations;

namespace BackEnd.DTOs.Categoria
{
    public class CategoriaInsertDTO
    {
        [StringLength(50)]
        public string? Nombre { get; set; }

        public bool Estado { get; set; }
    }
}
