namespace HotelWeb.Api.Models
{
    public class Hotel
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Direccion { get; set; }

        public string Ciudad { get; set; }

        public string Telefono { get; set; }

        public bool? Habilitado { get; set; }
    }
}
