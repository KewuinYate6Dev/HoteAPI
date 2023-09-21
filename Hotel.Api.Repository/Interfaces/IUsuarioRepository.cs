using HotelWeb.Api.Models;

namespace HotelWeb.Api.Repository.Interfaces
{
    public interface IUsuarioRepository
    {

        Task<Usuario> UsuarioById(int id);
        Task<Usuario> UsuarioByDocumentAndPassword(int documento, string password);

    }
}
