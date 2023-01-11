using System.ComponentModel.DataAnnotations;

namespace BackEnd.DTOs.Control
{
    public class ControlInsertDTO
    {
        [Required]
        public int IdTicket { get; set; }

        public bool? Estado { get; set; }
    }
}
