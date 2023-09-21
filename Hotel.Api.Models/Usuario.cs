namespace HotelWeb.Api.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        public string Nombres { get; set; } = null!;

        public string Apellidos { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Documento { get; set; } = null!;

        public short TipoDocumento { get; set; }

        public TipoUsuario TipoUsuario { get; set; }

        public string Constrasena { get; set; } = null!;
    }
}
