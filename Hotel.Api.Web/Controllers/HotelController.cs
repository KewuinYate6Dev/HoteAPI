using HotelWeb.Api.Models;
using HotelWeb.Api.Repository.Interfaces;
using HotelWeb.Api.Web.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelWeb.Api.Web.Controllers
{

    /// <summary>
    /// Controller que gestiona todas la logica de negocio de los hoteles
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(RolAuthorizationFilter), Arguments = new object[] { "admin" })]
    public class HotelController : ControllerBase
    {
        readonly IHotelRepository _hotelRepository;


        public HotelController(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        /// <summary>
        /// Se agrega una habitacion a la DB
        /// Campos NO obligatorios: Id, Habilitado
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Insert")]
        public IActionResult Insert([FromBody]Hotel hotel)
        {
            try
            {
                _hotelRepository.Agregar(hotel);
                return Ok(true);

            }catch (Exception ex)
            {
                return BadRequest(new
                {
                    ex.Message,
                });
            }
        }
        /// <summary>
        /// Se actualiza un hotel en la DB
        /// Campos NO obligatorios: Habilitado
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Actualizar")]
        public async Task<IActionResult> Actualizar([FromBody] Hotel hotel)
        {
            try
            {
                return Ok( await _hotelRepository.Actualizar(hotel));

            }catch(Exception ex)
            {
                return BadRequest(new
                {
                    ex.Message,
                });
            }
        }

        /// <summary>
        /// Se habilita o desabilita un hotel
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("ActualizarEstado")]
        public async Task<IActionResult> ActualizarEstado([FromBody] int id)
        {
            try
            {

                return Ok(await _hotelRepository.ActualizarEstado(id));
            }catch(Exception ex)
            {
                return BadRequest(new
                {
                    ex.Message,
                });
            }
        }


    }
}
