using HotelWeb.Api.Models;
using HotelWeb.Api.Repository.DAL;
using HotelWeb.Api.Repository.Interfaces;

namespace HotelWeb.Api.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private UsuarioData usuarioData;

        public UsuarioRepository(string connection)
        {
            usuarioData = new(connection);
        }

        public async Task<Usuario> UsuarioByDocumentAndPassword(int documento, string password)
        {
            return await usuarioData.UsuarioByDocumentAndPassword(documento, password);
        }

        public async Task<Usuario> UsuarioById(int id)
        {
            if (id == 0) throw new Exception("El id del usuario debe ser un valor valido");

            return await usuarioData.UsuarioById(id);
        }
    }
}
