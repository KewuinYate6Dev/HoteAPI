using HotelWeb.Api.Models;
using HotelWeb.Api.Repository.DAL;
using HotelWeb.Api.Repository.Interfaces;
using System.Data;

namespace HotelWeb.Api.Repository
{
    public class HabitacionRepository : IHabitacionRepository
    {
        private HabitacionData habitacionData;

        public HabitacionRepository(string connection)
        {
            habitacionData = new(connection);
        }

        public async Task<bool> ActualizarEstado(int id)
        {
            if (id > 0)
                return await habitacionData.ActualizarEstado(id);
            else
                throw new Exception("El id es obligatorio");
        }

        public async Task<bool> Agregar(Habitacion habitacion)
        {
            if(habitacion != null)
            {
                if (habitacion.Hotel == 0) throw new Exception("El campo hotel debe ser un numero valido");
                if (habitacion.CantidadPersonas == 0) throw new Exception("El campo cantidad personas debe ser un numero valido");
                if (habitacion.CostoBase == 0) throw new Exception("El campo costo base debe ser un numero valido");
                if (habitacion.TipoHabitacion == 0) throw new Exception("El campo tipo habitacion debe ser un numero valido");
                if (habitacion.Piso == 0) throw new Exception("El campo piso debe ser un numero valido");
                if (habitacion.TipoVista == 0) throw new Exception("El campo tipo vista debe ser un numero valido");
                habitacion.Impuestos = habitacion.CostoBase * 0.19;

                habitacion.Habilitado = 1;

                return await habitacionData.Agregar(habitacion);
            }
            return false;

            
        }

       
        public async Task<bool> Update(Habitacion habitacion)
        {
            if (habitacion != null && habitacion.Id > 0)
            {
                habitacion.Impuestos = habitacion.CostoBase * 0.19;

                return await habitacionData.Update(habitacion);

            }
            return false;
            
        }

        public async Task<List<HabitacionesAvaliable>> HabitacionesAvalaibleByHotel(string ciudad, int cantidadPersonas, DateTime llegada, DateTime salida)
        {
            if (string.IsNullOrEmpty(ciudad))
                throw new ArgumentNullException("La ciudad no puede ser vacia o null");
            if(cantidadPersonas <= 0)
                throw new ArgumentNullException("La cantidad de personas no puede ser 0");
            if (llegada > salida)
                throw new Exception("La fecha de llegada no puede ser mayor a la de la salida");

            return await habitacionData.HabitacionesAvalaibleByHotel(ciudad, cantidadPersonas, llegada, salida);
        }



    }
}
