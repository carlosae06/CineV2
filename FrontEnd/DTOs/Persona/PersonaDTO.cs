using System.ComponentModel.DataAnnotations;

namespace FrontEnd.DTOs.Persona
{
    public class PersonaDTO
    {
        public int Id { get; set; }
        public string Ci { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string ApellidoPaterno { get; set; } = null!;
        public string ApellidoMaterno { get; set; } = null!;
        public string? Telefono { get; set; }
    }
}
