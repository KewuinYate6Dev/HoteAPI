using HotelWeb.Api.Models;
using HotelWeb.Api.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelWeb.Api.Repository.DAL
{
    internal class HabitacionData: IHabitacionRepository
    {
        private ADO.Interfaces.IAdo DataInstance;

        public HabitacionData(string connection)
        {
            DataInstance = ADO.AdoFactory.GetADOInstance(connection);
        }

        public async Task<bool> ActualizarEstado(int id)
        {
            List<System.Data.IDbDataParameter> lstParams = new List<System.Data.IDbDataParameter> {
                DataInstance.CreateTypedParameter("p_id", id)
            };

            ADO.Models.AdoModelResponse response = await DataInstance.ExecuteQuery(new ADO.Models.AdoModelRequest()
            {
                CommandExecutionType = ADO.Models.EnumCommandExecutionType.DataTable,
                CommandText = "ActualizarEstadoHabitacion",
                CommandType = System.Data.CommandType.StoredProcedure,
                Parameters = lstParams
            });

            return response.NonQueryResult == 0;
        }

        public async Task<bool> Agregar(Habitacion habitacion)
        {
            
            List<System.Data.IDbDataParameter> lstParams = new List<System.Data.IDbDataParameter> {
                DataInstance.CreateTypedParameter("p_hotel", habitacion.Hotel),
                DataInstance.CreateTypedParameter("p_cantidad_personas", habitacion.CantidadPersonas),
                DataInstance.CreateTypedParameter("p_costo_base", habitacion.CostoBase),
                DataInstance.CreateTypedParameter("p_tipo_habitacion", habitacion.TipoHabitacion),
                DataInstance.CreateTypedParameter("p_piso", habitacion.Piso),
                DataInstance.CreateTypedParameter("p_tipo_vista", habitacion.TipoVista),
                DataInstance.CreateTypedParameter("p_impuestos", habitacion.Impuestos)
            };

            ADO.Models.AdoModelResponse response = await DataInstance.ExecuteQuery(new ADO.Models.AdoModelRequest()
            {
                CommandExecutionType = ADO.Models.EnumCommandExecutionType.DataTable,
                CommandText = "InsertarHabitacion",
                CommandType = System.Data.CommandType.StoredProcedure,
                Parameters = lstParams
            });

            return response.NonQueryResult == 0;
        }


        public async Task<bool> Update(Habitacion habitacion)
        {
            habitacion.Impuestos = habitacion.CostoBase * 0.19;


            List<System.Data.IDbDataParameter> lstParams = new List<System.Data.IDbDataParameter> {
                DataInstance.CreateTypedParameter("p_Id", habitacion.Id),
                DataInstance.CreateTypedParameter("p_hotel", habitacion.Hotel),
                DataInstance.CreateTypedParameter("p_cantidad_personas", habitacion.CantidadPersonas),
                DataInstance.CreateTypedParameter("p_costo_base", habitacion.CostoBase),
                DataInstance.CreateTypedParameter("p_tipo_habitacion", habitacion.TipoHabitacion),
                DataInstance.CreateTypedParameter("p_piso", habitacion.Piso),
                DataInstance.CreateTypedParameter("p_tipo_vista", habitacion.TipoVista),
                DataInstance.CreateTypedParameter("p_impuestos", habitacion.Impuestos)
            };

            ADO.Models.AdoModelResponse response = await DataInstance.ExecuteQuery(new ADO.Models.AdoModelRequest()
            {
                CommandExecutionType = ADO.Models.EnumCommandExecutionType.DataTable,
                CommandText = "ActualizarHabitacion",
                CommandType = System.Data.CommandType.StoredProcedure,
                Parameters = lstParams
            });

            return response.NonQueryResult == 0;
        }

        public async Task<List<HabitacionesAvaliable>> HabitacionesAvalaibleByHotel(string ciudad, int cantidadPersonas, DateTime llegada, DateTime salida)
        {
            List<HabitacionesAvaliable> habitacionesAvaliables = new();

            List<System.Data.IDbDataParameter> lstParams = new List<System.Data.IDbDataParameter> {
                DataInstance.CreateTypedParameter("p_ciudad", ciudad),
                DataInstance.CreateTypedParameter("p_cantidad_personas", cantidadPersonas),
                DataInstance.CreateTypedParameter("p_fecha_entrada", llegada),
                DataInstance.CreateTypedParameter("p_fecha_salida", salida),
            };

            ADO.Models.AdoModelResponse response = await DataInstance.ExecuteQuery(new ADO.Models.AdoModelRequest()
            {
                CommandExecutionType = ADO.Models.EnumCommandExecutionType.DataTable,
                CommandText = "HabitacionesAvailableByHotel",
                CommandType = System.Data.CommandType.StoredProcedure,
                Parameters = lstParams
            });

            if (response.DataTableResult != null &&
              response.DataTableResult.Rows.Count > 0)
            {

                habitacionesAvaliables = response.DataTableResult.AsEnumerable().Select(x => new HabitacionesAvaliable
                {
                    IdHotel = x.Field<int>("hotel_id"),
                    NombreHotel = x.Field<string>("nombre_hotel")!,
                    IdHabitacion = x.Field<int>("habitacion_id")!,
                    CantidadPersonas = x.Field<int>("capacidad_habitacion")!,
                    CiudadHotel = x.Field<string>("ciudad_hotel")!,
                    CostoBase = x.Field<int>("costo_base")!,
                    DireccionHotel = x.Field<string>("direccion_hotel")!,
                    Piso = x.Field<short>("piso")!,
                    TelefonoHotel = x.Field<string>("telefono_hotel")!,
                    TipoVista = x.Field<string>("tipo_vista")!

                }).ToList()!;

                return habitacionesAvaliables;

            }
            return null!;
        }
    }
}
