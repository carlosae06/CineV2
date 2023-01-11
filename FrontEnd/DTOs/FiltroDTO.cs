using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.DTOs
{
    public class FiltroDTO
    {
        public int Pagina { get; set; } = 1;
        public int CantidadRegistrosPorPagina { get; set; } = 20;


        public long UserId { get; set; }
        public string? Texto { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
    }

    public class FiltroHistoricoRegistroDTO : FiltroDTO
    {
        [Required]
        public int InscripcionCarreraId { get; set; }

    }

    public class FiltroFechaDTO : FiltroDTO
    {
        [Required]
        public DateTime Fecha { get; set; }
        public bool ConFechaFin { get; set; } = false;
        public DateTime FechaFin { get; set; }

    }

    public class FiltroGrupoOfertaDTO
    {
        [Required]
        public int InscripcionCarreraId { get; set; }
        [Required]
        public int OfertaId { get; set; }
        [Required]
        public int TurnoId { get; set; }
        [Required]
        public int SistemaEstudioId { get; set; }
    }


}
