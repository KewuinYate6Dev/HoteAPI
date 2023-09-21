using HotelWeb.Api.Models;

namespace HotelWeb.Api.Repository.Interfaces
{
    public interface IHabitacionRepository
    {

        Task<bool> Agregar(Habitacion habitacion); 
        Task<bool> Update(Habitacion habitacion);
        Task<bool> ActualizarEstado(int id);
        Task<List<HabitacionesAvaliable>> HabitacionesAvalaibleByHotel(string ciudad, int cantidadPersonas, DateTime llegada, DateTime salida);

    }
}
