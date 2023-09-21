namespace HotelWeb.Api.Services
{
using HotelWeb.Api.Services.Interfaces;
using MimeKit;
using MailKit.Net.Smtp;
    public class EmailService : IEmailService
    {
        public void EnviarCorreo(string destinatario, string asunto, string cuerpo)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Hotel API", "kevin.pruebanet6@gmail.com")); // Remitente
            message.To.Add(new MailboxAddress("", destinatario)); // Destinatario
            message.Subject = asunto;

            // Cuerpo del correo
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = cuerpo;
            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false); // Servidor SMTP y puerto
                client.Authenticate("kevin.pruebanet6@gmail.com", "anwlknwtaavwqlqb"); //anwl knwt aavw qlqb Tu dirección de correo electrónico y contraseña
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
