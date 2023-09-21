using HotelWeb.Api.Models;
using HotelWeb.Api.Repository.Interfaces;
using HotelWeb.Api.Web.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelWeb.Api.Web.Controllers
{
    /// <summary>
    /// Controller que gestiona todas la logica de negocio de las habitaciones
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HabitacionController : ControllerBase
    {
        readonly IHabitacionRepository _habitacionRepository;

        /// <summary>
        /// Controller que gestiona todas la logica de negocio de las habitaciones
        /// </summary>
        /// <param name="habitacionRepository"></param>
        public HabitacionController(IHabitacionRepository habitacionRepository)
        {
            _habitacionRepository = habitacionRepository;
        }

        /// <summary>
        /// Agrega una nueva habitacion a la DB
        /// Campos NO obligatorios: Id, Impuestos(se calcula valor CostoBase * 0.19), Habilitado
        /// </summary>
        /// <param name="habitacion"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("Agregar")]
        [TypeFilter(typeof(RolAuthorizationFilter), Arguments = new object[] { "admin" })]
        public async Task<IActionResult> Agregar([FromBody] Habitacion habitacion)
        {
            try
            {
                return Ok(await _habitacionRepository.Agregar(habitacion));
            }catch (Exception ex)
            {
                return BadRequest(new { 
                    ex.Message,
                });

            }

        }


        /// <summary>
        /// Actualiza una habitacion en la DB
        /// Campos no obligatorios Habilitado
        /// </summary>
        /// <param name="habitacion"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Actualizar")]
        [TypeFilter(typeof(RolAuthorizationFilter), Arguments = new object[] { "admin" })]
        public async Task<IActionResult> Actualizar([FromBody] Habitacion habitacion)
        {
            try
            {
                return Ok(await _habitacionRepository.Update(habitacion));

            }catch (Exception ex)
            {
                return BadRequest(new
                {
                    ex.Message,
                });
            }

        }

        /// <summary>
        /// Habilita o desabilita una habitacion
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ActualizarEstado")]
        [TypeFilter(typeof(RolAuthorizationFilter), Arguments = new object[] { "admin" })]
        public async Task<IActionResult> ActualizarEstado([FromBody]int id)
        {
            try
            {
                return Ok(await _habitacionRepository.ActualizarEstado(id));
            }catch (Exception ex)
            {
                return BadRequest(new
                {
                    ex.Message,
                });
            }

        }


        /// <summary>
        /// Busca las habitaciones disponibles por ciudad, por cantidad de personas, dia de llegada y salida
        /// la ciudad no debe ser escrita completamente puedes ir buscando Med, mede, medellin
        /// </summary>
        /// <param name="ciudad"></param>
        /// <param name="cantidadPersonas"></param>
        /// <param name="llegada"></param>
        /// <param name="salida"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("HabitacionAvailableByHotel")]
        [TypeFilter(typeof(RolAuthorizationFilter), Arguments = new object[] { "cliente" })]
        public async Task<IActionResult> HabitacionAvailableByHotel([FromQuery] string ciudad, [FromQuery] int cantidadPersonas, [FromQuery] DateTime llegada, [FromQuery] DateTime salida)
        {
            try
            {
                return Ok( await _habitacionRepository.HabitacionesAvalaibleByHotel(ciudad, cantidadPersonas, llegada, salida));

            }catch (Exception ex)
            {
                return BadRequest(new
                {
                    ex.Message,
                });
            }

        }

    }
}
