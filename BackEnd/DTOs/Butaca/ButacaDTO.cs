using System.ComponentModel.DataAnnotations;

namespace BackEnd.DTOs.Butaca
{
    public class ButacaDTO
    {
        [Required]
        public int Id { get; set; }

        public bool? Estado { get; set; }
        [Required]
        public int IdSeccion { get; set; }
    }
}
