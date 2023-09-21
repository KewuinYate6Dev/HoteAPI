using HotelWeb.Api.Models;
using HotelWeb.Api.Repository.Interfaces;
using System.Data;

namespace HotelWeb.Api.Repository.DAL
{
    internal class ReservaData: IReservaRepository
    {
        private ADO.Interfaces.IAdo DataInstance;

        public ReservaData(string connection)
        {
            DataInstance = ADO.AdoFactory.GetADOInstance(connection);
        }


        public async Task<Reserva> Reservar(Reserva reserva)
        {
            Reserva newReserva = new();
            List<System.Data.IDbDataParameter> lstParams = new List<System.Data.IDbDataParameter> {
                DataInstance.CreateTypedParameter("p_habitacion", reserva.Habitacion),
                DataInstance.CreateTypedParameter("p_usuario", reserva.Usuario),
                DataInstance.CreateTypedParameter("p_llegada", reserva.Llegada),
                DataInstance.CreateTypedParameter("p_salida", reserva.Salida),
                DataInstance.CreateTypedParameter("p_noches", reserva.Noches),
                DataInstance.CreateTypedParameter("p_total", reserva.Total),
                DataInstance.CreateTypedParameter("p_estado", reserva.Estado),
            };

            ADO.Models.AdoModelResponse response = await DataInstance.ExecuteQuery(new ADO.Models.AdoModelRequest()
            {
                CommandExecutionType = ADO.Models.EnumCommandExecutionType.DataTable,
                CommandText = "InsertarReserva",
                CommandType = System.Data.CommandType.StoredProcedure,
                Parameters = lstParams
            });
            if (response.DataTableResult != null &&
              response.DataTableResult.Rows.Count > 0)
            {

                newReserva = response.DataTableResult.AsEnumerable().Select(x => new Reserva
                {
                    Estado = x.Field<short>("estado"),
                    Habitacion = x.Field<int>("habitacion"),
                    Id = x.Field<int>("id"),
                    Llegada = x.Field<DateTime>("llegada"),
                    Noches = x.Field<int>("noches"),
                    Salida = x.Field<DateTime>("salida"),
                    Total = x.Field<int>("total"),
                    Usuario = x.Field<int>("usuario")
                }).FirstOrDefault()!;

                newReserva.Huespedes = AgregarHuespedes(reserva.Huespedes, newReserva.Id);
                newReserva.ContactoEmergencia = await AgregarContacto(reserva.ContactoEmergencia, newReserva.Id);
            }

            return newReserva;

        }



        public async Task<List<ReservaDTO>> ReservasByHotel(int idHotel)
        {
            List<ReservaDTO> newReserva = new();
            List<System.Data.IDbDataParameter> lstParams = new List<System.Data.IDbDataParameter> {
                DataInstance.CreateTypedParameter("hotel_id", idHotel),
            };

            ADO.Models.AdoModelResponse response = await DataInstance.ExecuteQuery(new ADO.Models.AdoModelRequest()
            {
                CommandExecutionType = ADO.Models.EnumCommandExecutionType.DataTable,
                CommandText = "ListarReservasPorHotel",
                CommandType = System.Data.CommandType.StoredProcedure,
                Parameters = lstParams
            });
            if (response.DataTableResult != null &&
              response.DataTableResult.Rows.Count > 0)
            {

                newReserva = response.DataTableResult.AsEnumerable().Select(x => new ReservaDTO
                {
                    Estado = x.Field<short>("estado"),
                    Habitacion = x.Field<int>("habitacion"),
                    Id = x.Field<int>("id"),
                    Llegada = x.Field<DateTime>("llegada"),
                    Noches = x.Field<int>("noches"),
                    Salida = x.Field<DateTime>("salida"),
                    Total = x.Field<int>("total"),
                    Usuario = x.Field<int>("usuario"),
                    CantidadPersonas = int.Parse(x.Field<long>("cant_huespedes").ToString()),
                }).ToList()!;

            }

            return newReserva;
        }

        public async Task<Reserva> ReservasById(int idReserva)
        {
            Reserva reserva = new();
            List<System.Data.IDbDataParameter> lstParams = new List<System.Data.IDbDataParameter> {
                DataInstance.CreateTypedParameter("reserva_id", idReserva),
            };

            ADO.Models.AdoModelResponse response = await DataInstance.ExecuteQuery(new ADO.Models.AdoModelRequest()
            {
                CommandExecutionType = ADO.Models.EnumCommandExecutionType.DataTable,
                CommandText = "ReservaById",
                CommandType = System.Data.CommandType.StoredProcedure,
                Parameters = lstParams
            });
            if (response.DataTableResult != null &&
              response.DataTableResult.Rows.Count > 0)
            {

                reserva = response.DataTableResult.AsEnumerable().Select(x => new Reserva
                {
                    Estado = x.Field<short>("estado"),
                    Habitacion = x.Field<int>("habitacion"),
                    Id = x.Field<int>("id"),
                    Llegada = x.Field<DateTime>("llegada"),
                    Noches = x.Field<int>("noches"),
                    Salida = x.Field<DateTime>("salida"),
                    Total = x.Field<int>("total"),
                    Usuario = x.Field<int>("usuario"),
                }).FirstOrDefault()!;

                reserva.ContactoEmergencia = await SearchContactoByReserva(reserva.Id);
                reserva.Huespedes = await SearchHuespedesByReserva(reserva.Id);


            }

            return reserva;
        }


        #region Private
        private List<Huesped> AgregarHuespedes(List<Huesped> huespedes, int idReserva)
        {
            if (huespedes != null && huespedes.Any())
            {
                List<Huesped> huespedesList = new();
                ADO.Models.AdoModelResponse responseHuespedes = null!;
                huespedes.ForEach(async x =>
                {
                    List<System.Data.IDbDataParameter> lstParams = new() {
                        DataInstance.CreateTypedParameter("p_nombres", x.Nombres),
                        DataInstance.CreateTypedParameter("p_apellidos", x.Apellidos),
                        DataInstance.CreateTypedParameter("p_documento", x.Documento),
                        DataInstance.CreateTypedParameter("p_edad", x.Edad),
                        DataInstance.CreateTypedParameter("p_reserva", idReserva),
                        DataInstance.CreateTypedParameter("p_fecha_nacimiento", x.FechaNacimiento),
                        DataInstance.CreateTypedParameter("p_genero", x.Genero),
                        DataInstance.CreateTypedParameter("p_tipo_documento", x.TipoDocumento),
                        DataInstance.CreateTypedParameter("p_email", x.Email),
                        DataInstance.CreateTypedParameter("p_telefono", x.Telefono),
                        };

                    responseHuespedes = await DataInstance.ExecuteQuery(new ADO.Models.AdoModelRequest()
                    {
                        CommandExecutionType = ADO.Models.EnumCommandExecutionType.DataTable,
                        CommandText = "InsertarHuesped",
                        CommandType = System.Data.CommandType.StoredProcedure,
                        Parameters = lstParams
                    });

                });
                if (responseHuespedes!.DataTableResult != null && responseHuespedes.DataTableResult.Rows.Count > 0)
                {
                    huespedesList = responseHuespedes.DataTableResult.AsEnumerable().Select(x => new Huesped
                    {
                        Edad = x.Field<string>("edad")!,
                        Apellidos = x.Field<string>("apellidos")!,
                        Documento = x.Field<string>("documento")!,
                        Nombres = x.Field<string>("nombres")!,
                        Reserva = x.Field<int>("reserva"),
                        FechaNacimiento = x.Field<DateTime>("fecha_nacimiento")!,
                        Genero = x.Field<string>("genero")!,
                        TipoDocumento = x.Field<short>("tipo_documento")!,
                        Email = x.Field<string>("email")!,
                        Telefono = x.Field<string>("telefono")!,

                    }).ToList()!;
                }

                return huespedesList;
            }
            return null!;
        }

        private async Task<List<Huesped>> SearchHuespedesByReserva(int idReserva)
        {
            List<Huesped> huespedesList = new();
            ADO.Models.AdoModelResponse responseHuespedes = null!;

            List<System.Data.IDbDataParameter> lstParams = new() {
                    DataInstance.CreateTypedParameter("reserva_id", idReserva)
                    };

            responseHuespedes = await DataInstance.ExecuteQuery(new ADO.Models.AdoModelRequest()
            {
                CommandExecutionType = ADO.Models.EnumCommandExecutionType.DataTable,
                CommandText = "HuespedesByReservaId",
                CommandType = System.Data.CommandType.StoredProcedure,
                Parameters = lstParams
            });

            if (responseHuespedes!.DataTableResult != null && responseHuespedes.DataTableResult.Rows.Count > 0)
            {
                huespedesList = responseHuespedes.DataTableResult.AsEnumerable().Select(x => new Huesped
                {
                    Edad = x.Field<string>("edad")!,
                    Apellidos = x.Field<string>("apellidos")!,
                    Documento = x.Field<string>("documento")!,
                    Nombres = x.Field<string>("nombres")!,
                    Reserva = x.Field<int>("reserva"),
                    FechaNacimiento = x.Field<DateTime>("fecha_nacimiento")!,
                    Genero = x.Field<string>("genero")!,
                    TipoDocumento = x.Field<short>("tipo_documento")!,
                    Email = x.Field<string>("email")!,
                    Telefono = x.Field<string>("telefono")!,

                }).ToList()!;
            }

            return huespedesList;

        }

        private async Task<ContactoEmergencia> AgregarContacto(ContactoEmergencia contacto, int idReserva)
        {

            ContactoEmergencia contactoEmergencia = new();

            List<System.Data.IDbDataParameter> lstParams = new List<System.Data.IDbDataParameter> {
                DataInstance.CreateTypedParameter("p_nombres", contacto.Nombres),
                DataInstance.CreateTypedParameter("p_apellidos", contacto.Apellidos),
                DataInstance.CreateTypedParameter("p_reserva", idReserva),
                DataInstance.CreateTypedParameter("p_telefono", contacto.Telefono),
            };

            ADO.Models.AdoModelResponse response = await DataInstance.ExecuteQuery(new ADO.Models.AdoModelRequest()
            {
                CommandExecutionType = ADO.Models.EnumCommandExecutionType.DataTable,
                CommandText = "InsertarContactoEmergencia",
                CommandType = System.Data.CommandType.StoredProcedure,
                Parameters = lstParams
            });
            if (response.DataTableResult != null &&
              response.DataTableResult.Rows.Count > 0)
            {

                contactoEmergencia = response.DataTableResult.AsEnumerable().Select(x => new ContactoEmergencia
                {
                    Nombres = x.Field<string>("nombres")!,
                    Apellidos = x.Field<string>("apellidos")!,
                    Reserva = x.Field<int>("reserva"),
                    Telefono = x.Field<string>("telefono")!,
                }).FirstOrDefault()!;



            }
            return contactoEmergencia;
        }

        private async Task<ContactoEmergencia> SearchContactoByReserva(int idReserva)
        {

            ContactoEmergencia contactoEmergencia = new();

            List<System.Data.IDbDataParameter> lstParams = new List<System.Data.IDbDataParameter> {
                DataInstance.CreateTypedParameter("reserva_id", idReserva),
            };

            ADO.Models.AdoModelResponse response = await DataInstance.ExecuteQuery(new ADO.Models.AdoModelRequest()
            {
                CommandExecutionType = ADO.Models.EnumCommandExecutionType.DataTable,
                CommandText = "ContEmergenciaByReservaId",
                CommandType = System.Data.CommandType.StoredProcedure,
                Parameters = lstParams
            });
            if (response.DataTableResult != null &&
              response.DataTableResult.Rows.Count > 0)
            {

                contactoEmergencia = response.DataTableResult.AsEnumerable().Select(x => new ContactoEmergencia
                {
                    Nombres = x.Field<string>("nombres")!,
                    Apellidos = x.Field<string>("apellidos")!,
                    Reserva = x.Field<int>("reserva"),
                    Telefono = x.Field<string>("telefono")!,
                }).FirstOrDefault()!;



            }
            return contactoEmergencia;
        }


        #endregion
    }
}
