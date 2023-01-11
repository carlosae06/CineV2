namespace FrontEnd.DTOs
{
    public class UserAuth
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string CargoNombre { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string Rol { get; set; } = string.Empty;
        public long Expiracion { get; set; }
        public string Token { get; set; } = string.Empty;

    }
}
