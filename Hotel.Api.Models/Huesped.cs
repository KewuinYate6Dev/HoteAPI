namespace HotelWeb.Api.Models
{
    public class Huesped
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Documento { get; set; }
        public string Edad { get; set; }
        public int Reserva { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Genero { get; set; }
        public short TipoDocumento { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }


    }

}
