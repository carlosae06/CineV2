namespace FrontEnd.DTOs
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string UserName { get; set; } = null!;        
        public int RolId { get; set; }
        public string RolCod { get; set; } = string.Empty!;
        public string RolNombre { get; set; } = string.Empty!;
        public bool Estado { get; set; }
    }
}
