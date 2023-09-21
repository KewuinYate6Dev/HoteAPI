
namespace HotelWeb.Api.Models
{
    public class HabitacionesAvaliable
    {
        public int IdHotel { get; set; }
        public string NombreHotel { get; set; }
        public int IdHabitacion { get; set; }
        public int CantidadPersonas { get; set; }
        public string DireccionHotel { get; set; }
        public string CiudadHotel { get; set; }
        public string TelefonoHotel { get; set; }
        public int CostoBase { get; set; }
        public short Piso { get; set; }
        public string TipoVista { get; set; }
    }
}
