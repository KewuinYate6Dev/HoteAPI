using HotelWeb.Api.Repository.Interfaces;
using HotelWeb.Api.Repository;
using HotelWeb.Api.Services.Interfaces;
using HotelWeb.Api.Services;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using Newtonsoft.Json;

namespace HotelWeb.Api.Web.Util
{
    public static class ServicesExtensions
    {

        public static void AddServicesHotel(this IServiceCollection services, string connectionMysql)
        {
            //services.AddScoped<RolAuthorizationFilter>();
            services.AddScoped<IHotelRepository>(inj => new HotelRepository(connectionMysql!));
            services.AddScoped<IHabitacionRepository>(inj => new HabitacionRepository(connectionMysql!));
            services.AddScoped<IReservaRepository>(inj => new ReservaRepository(connectionMysql!));
            services.AddScoped<IUsuarioRepository>(inj => new UsuarioRepository(connectionMysql!));
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IJwtService, JwtService>();
        }

        public static void AddJwtBearerHotel(this IServiceCollection services, string secretBusqo)
        {

            byte[] key = Encoding.ASCII.GetBytes(secretBusqo);

            services.AddAuthentication(c =>
            {
                c.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                c.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer((options) =>
            {
                options.RequireHttpsMetadata = false; // Cambiar a true en producción para requerir HTTPS
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false, // Puedes cambiar estos valores si quieres validar el emisor y/o el público objetivo del token
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

            });
        }

    }
}
