namespace HotelWeb.Api.Services.Interfaces
{
    public interface IEmailService
    {
        void EnviarCorreo(string destinatario, string asunto, string cuerpo);
    }
}
