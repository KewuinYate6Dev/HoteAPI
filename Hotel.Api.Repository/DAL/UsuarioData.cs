using HotelWeb.Api.Models;
using HotelWeb.Api.Repository.Interfaces;
using System.Data;

namespace HotelWeb.Api.Repository.DAL
{
    public class UsuarioData : IUsuarioRepository
    {
        private ADO.Interfaces.IAdo DataInstance;

        public UsuarioData(string connection)
        {
            DataInstance = ADO.AdoFactory.GetADOInstance(connection);
        }

        public async Task<Usuario> UsuarioByDocumentAndPassword(int documento, string password)
        {
            Usuario usuario = new();
            List<System.Data.IDbDataParameter> lstParams = new List<System.Data.IDbDataParameter> {
                DataInstance.CreateTypedParameter("p_documento", documento),
                DataInstance.CreateTypedParameter("p_password", password),
            };

            ADO.Models.AdoModelResponse response = await DataInstance.ExecuteQuery(new ADO.Models.AdoModelRequest()
            {
                CommandExecutionType = ADO.Models.EnumCommandExecutionType.DataTable,
                CommandText = "UsuarioByDocumentAndPassword",
                CommandType = System.Data.CommandType.StoredProcedure,
                Parameters = lstParams
            });

            if (response.DataTableResult != null &&
              response.DataTableResult.Rows.Count > 0)
            {

                usuario = response.DataTableResult.AsEnumerable().Select(x => new Usuario
                {
                    Id = x.Field<int>("id"),
                    Nombres = x.Field<string>("nombres")!,
                    Apellidos = x.Field<string>("apellidos")!,
                    Email = x.Field<string>("email")!,
                    Documento = x.Field<string>("documento")!,
                    TipoUsuario = new() { Nombres = x.Field<string>("tipo_usuario")!, }

                }).FirstOrDefault()!;

                return usuario;
            }
            return null!;

        }

        public async Task<Usuario> UsuarioById(int id)
        {
            Usuario usuario = new();
            List<System.Data.IDbDataParameter> lstParams = new List<System.Data.IDbDataParameter> {
                DataInstance.CreateTypedParameter("p_usuario", id),
            };

            ADO.Models.AdoModelResponse response = await DataInstance.ExecuteQuery(new ADO.Models.AdoModelRequest()
            {
                CommandExecutionType = ADO.Models.EnumCommandExecutionType.DataTable,
                CommandText = "UsuarioById",
                CommandType = System.Data.CommandType.StoredProcedure,
                Parameters = lstParams
            });

            if (response.DataTableResult != null &&
              response.DataTableResult.Rows.Count > 0)
            {

                usuario = response.DataTableResult.AsEnumerable().Select(x => new Usuario
                {
                    Id = x.Field<int>("id"),
                    Email = x.Field<string>("email")!,
                    Nombres = x.Field<string>("nombres")!,
                    Apellidos = x.Field<string>("apellidos")!,

                }).FirstOrDefault()!;

            }

            return usuario;
        }
    }
}
