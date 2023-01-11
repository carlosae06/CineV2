using System.ComponentModel.DataAnnotations;

namespace BackEnd.DTOs.Control
{
    public class ControlDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int IdTicket { get; set; }

        public bool? Estado { get; set; }
    }
}
