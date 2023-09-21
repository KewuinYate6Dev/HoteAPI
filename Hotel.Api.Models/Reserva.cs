using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HotelWeb.Api.Models
{
    public class Reserva
    {
        /// <summary>
        /// Esta propiedad es asignada al realizar un insert
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Id de la habitacion que se selecciono
        /// </summary>
        public int Habitacion { get; set; }


        /// <summary>
        /// Id del usuario que solicito o solicita la reserva
        /// </summary>
        public int Usuario { get; set; }
        
        public DateTime? Llegada { get; set; }
        public DateTime? Salida { get; set; }
        public int? Noches { get; set; }

        public int? Total { get; set; }

        public short? Estado { get; set; }

        public List<Huesped> Huespedes { get; set; }

        public ContactoEmergencia ContactoEmergencia { get; set; }
    }
}
