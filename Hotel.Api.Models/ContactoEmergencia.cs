using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelWeb.Api.Models
{
    public class ContactoEmergencia
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int Reserva { get; set; }
        public string Telefono { get; set; }
    }
}
