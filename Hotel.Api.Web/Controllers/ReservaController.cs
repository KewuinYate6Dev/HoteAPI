using HotelWeb.Api.Models;
using HotelWeb.Api.Repository.Interfaces;
using HotelWeb.Api.Services.Interfaces;
using HotelWeb.Api.Web.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelWeb.Api.Web.Controllers
{
    /// <summary>
    /// Controller que gestiona todas la logica de negocio de las reservas
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : ControllerBase
    {

        private readonly IReservaRepository _reservaRepository;

        

        public ReservaController(IReservaRepository reservaRepository)
        {
            _reservaRepository = reservaRepository;
        }

        /// <summary>
        /// Registra un reserva en la DB
        /// campos NO obligatorios: Noches(se calcula automaticamente, 
        /// Total(Se calcula automaticamente CostoBase + Impuestos * Noches), Estado)
        /// Huspedes.Id, ContactoEmergencia.Id
        /// </summary>
        /// <param name="reserva"></param>
        /// <param name="_emailService"></param>
        /// <param name="_usuarioRepository"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Reserva")]
        [TypeFilter(typeof(RolAuthorizationFilter), Arguments = new object[] { "cliente" })]
        public async Task<IActionResult> Reserva([FromBody] Reserva reserva, [FromServices] IEmailService _emailService, [FromServices] IUsuarioRepository _usuarioRepository)
        {
            try
            {
                Reserva response =  await _reservaRepository.Reservar(reserva);

                Usuario usuario = await _usuarioRepository.UsuarioById(reserva.Usuario);

                string textoEmail = $"Usted realizo una reserva para el dia {response.Llegada!.Value.ToString("yyyy-MM-dd")} " +
                    $"con dia de salida {response.Salida!.Value.ToString("yyyy-MM-dd")} \n Esperamos que disfrute de su estadia.";

                _emailService.EnviarCorreo(usuario.Email, "Reserva Agendada en Hotel API", textoEmail);

                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    ex.Message,
                });

            }

        }


        /// <summary>
        /// Consultar las reservas por hotel 
        /// </summary>
        /// <param name="idHotel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ReservasByHotel")]
        [TypeFilter(typeof(RolAuthorizationFilter), Arguments = new object[] { "admin" })]
        public async Task<IActionResult> ReservaByHotel([FromBody] int idHotel)
        {
            try
            {
                return Ok(await _reservaRepository.ReservasByHotel(idHotel));

            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    ex.Message,
                });

            }
        }

        /// <summary>
        /// Consultar las reservas por id
        /// </summary>
        /// <param name="idReserva"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ReservasById")]
        [TypeFilter(typeof(RolAuthorizationFilter), Arguments = new object[] { "admin" })]
        public async Task<IActionResult> ReservaById([FromBody] int idReserva)
        {
            try
            {
                return Ok(await _reservaRepository.ReservasById(idReserva));

            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    ex.Message,
                });

            }

        }
    }
}
