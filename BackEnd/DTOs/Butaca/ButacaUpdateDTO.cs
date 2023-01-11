using System.ComponentModel.DataAnnotations;

namespace BackEnd.DTOs.Butaca
{
    public class ButacaUpdateDTO
    {

        public bool? Estado { get; set; }
        [Required]
        public int IdSeccion { get; set; }
    }
}
