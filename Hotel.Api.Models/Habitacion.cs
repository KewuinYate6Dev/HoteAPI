using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelWeb.Api.Models
{
    public class Habitacion
    {
        public int Id { get; set; }

        public int Hotel { get; set; }

        public int CantidadPersonas { get; set; }

        public int CostoBase { get; set; }

        public double Impuestos { get; set; }

        public int TipoHabitacion { get; set; }

        public short? Piso { get; set; }

        public int TipoVista { get; set; }
        public int Habilitado { get; set; }
    }
}
