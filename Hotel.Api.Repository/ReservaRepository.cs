using HotelWeb.Api.Models;
using HotelWeb.Api.Repository.DAL;
using HotelWeb.Api.Repository.Interfaces;

namespace HotelWeb.Api.Repository
{
    public class ReservaRepository : IReservaRepository
    {
        private ReservaData reservaData;

        public ReservaRepository(string connection)
        {
            reservaData = new(connection);
        }


        public async Task<Reserva> Reservar(Reserva reserva)
        {
            if (reserva == null) throw new Exception("Reserva es null");
            if (reserva.Habitacion <= 0) throw new Exception("Habitacion debe tener un valor valido");
            if (reserva.Usuario <= 0) throw new Exception("Usuario debe tener un valor valido");
            if (reserva.Llegada > reserva.Salida) throw new Exception("La fecha de llegada no puede ser mayor a la de salida");

            reserva.Noches = CalcularNoches(reserva.Llegada!.Value, reserva.Salida!.Value);

            return await reservaData.Reservar(reserva);

        }

        public async Task<List<ReservaDTO>> ReservasByHotel(int idHotel)
        {
            if (idHotel == 0) throw new Exception("El id del hotel debe ser un valor valido");

            return await reservaData.ReservasByHotel(idHotel);
        }

        public async Task<Reserva> ReservasById(int idReserva)
        {
            if (idReserva == 0) throw new Exception("El id del hotel debe ser un valor valido");
            return await reservaData.ReservasById(idReserva);
        }

        private int CalcularNoches(DateTime fechaLlegada, DateTime fechaSalida)
        {
            TimeSpan diferencia = fechaSalida - fechaLlegada;

            if (diferencia.Days < 0)
            {
                throw new ArgumentException("La fecha de salida debe ser posterior a la fecha de llegada.");
            }

            return diferencia.Days;
        }

    }
}
