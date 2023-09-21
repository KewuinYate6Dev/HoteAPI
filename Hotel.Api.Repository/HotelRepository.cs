using HotelWeb.Api.Models;
using HotelWeb.Api.Repository.DAL;
using HotelWeb.Api.Repository.Interfaces;

namespace HotelWeb.Api.Repository
{


    public class HotelRepository : IHotelRepository
    {

        private HotelData hotelData;

        public HotelRepository(string connection)
        {
            hotelData = new(connection);
        }

        public async Task<bool> Actualizar(Hotel hotel)
        {
            if(hotel.Id <= 0) throw new ArgumentOutOfRangeException("Id no puede ser 0");
            ValidarHotel(hotel);
            return await hotelData.Actualizar(hotel);
        }

        public async Task<bool> ActualizarEstado(int idHotel)
        {
            if (idHotel <= 0) throw new ArgumentOutOfRangeException("El id del hotel tiene que ser mayor a 0");
            return await hotelData.ActualizarEstado(idHotel);
        }

        public void Agregar(Hotel hotel)
        {
            ValidarHotel(hotel);
            hotelData.Agregar(hotel);
        }

        public async Task<List<Hotel>> HotelesByCity(string ciudad)
        {
            if (string.IsNullOrEmpty(ciudad)) throw new ArgumentNullException("el valor ciudad no puede ser null o vacio");
            return await hotelData.HotelesByCity(ciudad);
        }

        private void ValidarHotel(Hotel hotel)
        {
            if (hotel == null) throw new ArgumentNullException("hotel no puede ser null");
            if (string.IsNullOrEmpty(hotel.Nombre)) throw new ArgumentNullException("nombre no puede ser null o vacia");
            if (string.IsNullOrEmpty(hotel.Ciudad)) throw new ArgumentNullException("Ciudad no puede ser null o vacia");
            if (string.IsNullOrEmpty(hotel.Telefono)) throw new ArgumentNullException("Telefono no puede ser null o vacia");
            if (string.IsNullOrEmpty(hotel.Direccion)) throw new ArgumentNullException("Direccion no puede ser null o vacia");
        }
    }
}
