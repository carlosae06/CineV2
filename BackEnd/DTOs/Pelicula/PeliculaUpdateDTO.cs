using System.ComponentModel.DataAnnotations;

namespace BackEnd.DTOs.Pelicula
{
    public class PeliculaUpdateDTO
    {
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; } = null!;
        [Required]
        [StringLength(50)]
        public string Clasificacion { get; set; } = null!;
        [Required]
        public DateTime FechaEstreno { get; set; }
        [Required]
        [StringLength(400)]
        public string Protagonista { get; set; } = null!;
        [Required]
        [StringLength(100)]
        public string Director { get; set; } = null!;
        [Required]
        [StringLength(50)]
        public string Idioma { get; set; } = null!;
        [Required]
        [StringLength(30)]
        public string Duracion { get; set; } = null!;
        [StringLength(400)]
        public string? Sinopsis { get; set; }

        public bool? Estado { get; set; }
        [Required]
        public int IdCategoria { get; set; }
    }
}
