namespace FrontEnd.DTOs
{
    public class AutorDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Documento { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }
        public string UrlBiografia { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string? Naciolidad { get; set; }
        public int CantidadLibros { get; set; }
        public string NombreDocumento { get; set; } = null!;
        public int Edad { get; set; }
        public bool Estado { get; set; }
    }
}
