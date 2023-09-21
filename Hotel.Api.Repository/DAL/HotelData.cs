using HotelWeb.Api.Models;
using HotelWeb.Api.Repository.Interfaces;
using System.Data;

namespace HotelWeb.Api.Repository.DAL
{
    internal class HotelData: IHotelRepository
    {
        private ADO.Interfaces.IAdo DataInstance;

        public HotelData(string connection)
        {
            DataInstance = ADO.AdoFactory.GetADOInstance(connection);
        }

        public async Task<bool> Actualizar(Hotel hotel)
        {
            List<System.Data.IDbDataParameter> lstParams = new List<System.Data.IDbDataParameter> {
                DataInstance.CreateTypedParameter("p_id", hotel.Id),
                DataInstance.CreateTypedParameter("p_nombre", hotel.Nombre),
                DataInstance.CreateTypedParameter("p_direccion", hotel.Direccion),
                DataInstance.CreateTypedParameter("p_ciudad", hotel.Ciudad),
                DataInstance.CreateTypedParameter("p_telefono", hotel.Telefono),
                DataInstance.CreateTypedParameter("p_habilitado", hotel.Habilitado)
            };

            ADO.Models.AdoModelResponse response = await DataInstance.ExecuteQuery(new ADO.Models.AdoModelRequest()
            {
                CommandExecutionType = ADO.Models.EnumCommandExecutionType.DataTable,
                CommandText = "ActualizarHotel",
                CommandType = System.Data.CommandType.StoredProcedure,
                Parameters = lstParams
            });

            return response.NonQueryResult == 0;
        }

        public async Task<bool> ActualizarEstado(int idHotel)
        {
            List<System.Data.IDbDataParameter> lstParams = new List<System.Data.IDbDataParameter> {
                DataInstance.CreateTypedParameter("p_id", idHotel)
            };

            ADO.Models.AdoModelResponse response = await DataInstance.ExecuteQuery(new ADO.Models.AdoModelRequest()
            {
                CommandExecutionType = ADO.Models.EnumCommandExecutionType.DataTable,
                CommandText = "ActualizarEstadoHotel",
                CommandType = System.Data.CommandType.StoredProcedure,
                Parameters = lstParams
            });

            return response.NonQueryResult == 0;
        }

        public async void Agregar(Hotel hotel)
        {
            List<System.Data.IDbDataParameter> lstParams = new List<System.Data.IDbDataParameter> {
                DataInstance.CreateTypedParameter("p_nombre", hotel.Nombre),
                DataInstance.CreateTypedParameter("p_direccion", hotel.Direccion),
                DataInstance.CreateTypedParameter("p_ciudad", hotel.Ciudad),
                DataInstance.CreateTypedParameter("p_telefono", hotel.Telefono),
                DataInstance.CreateTypedParameter("p_habilitado", hotel.Habilitado)
            };

            ADO.Models.AdoModelResponse response = await DataInstance.ExecuteQuery(new ADO.Models.AdoModelRequest()
            {
                CommandExecutionType = ADO.Models.EnumCommandExecutionType.DataTable,
                CommandText = "InsertarHotel",
                CommandType = System.Data.CommandType.StoredProcedure,
                Parameters = lstParams
            });
        }

        public async Task<List<Hotel>> HotelesByCity(string ciudad)
        {
            List<Hotel> hoteles = new();

            List<System.Data.IDbDataParameter> lstParams = new List<System.Data.IDbDataParameter> {
                DataInstance.CreateTypedParameter("p_ciudad", ciudad),
            };

            ADO.Models.AdoModelResponse response = await DataInstance.ExecuteQuery(new ADO.Models.AdoModelRequest()
            {
                CommandExecutionType = ADO.Models.EnumCommandExecutionType.DataTable,
                CommandText = "HotelesByCity",
                CommandType = System.Data.CommandType.StoredProcedure,
                Parameters = lstParams
            });

            if (response.DataTableResult != null &&
              response.DataTableResult.Rows.Count > 0)
            {

                hoteles = response.DataTableResult.AsEnumerable().Select(x => new Hotel
                {
                    Id = x.Field<int>("id"),
                    Ciudad = x.Field<string>("ciudad")!,
                    Nombre = x.Field<string>("nombre")!,
                    Direccion = x.Field<string>("direccion")!,
                    Telefono = x.Field<string>("telefono")!,
                    Habilitado = x.Field<bool>("habilitado"),
                }).ToList()!;

                return hoteles;

            }
            return null!;
        }
    }
}
