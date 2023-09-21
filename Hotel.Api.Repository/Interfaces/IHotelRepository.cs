using HotelWeb.Api.Models;

namespace HotelWeb.Api.Repository.Interfaces
{
    public interface IHotelRepository 
    {
        void Agregar(Hotel hotel);

        Task<bool> Actualizar(Hotel hotel);

        Task<bool> ActualizarEstado(int idHotel);

        Task<List<Hotel>> HotelesByCity(string ciudad);
    }
}
