using HotelWeb.Api.Models;

namespace HotelWeb.Api.Repository.Interfaces
{
    public interface IReservaRepository
    {

        Task<Reserva> Reservar(Reserva reserva);

        Task<List<ReservaDTO>> ReservasByHotel(int idHotel);
        Task<Reserva> ReservasById(int idReserva);

    }
}
