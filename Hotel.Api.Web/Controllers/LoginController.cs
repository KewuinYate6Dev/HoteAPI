using HotelWeb.Api.Models;
using HotelWeb.Api.Models.Util;
using HotelWeb.Api.Repository.Interfaces;
using HotelWeb.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace HotelWeb.Api.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly IJwtService _jwtService;
        private readonly AppSettings _appSettings;
        private readonly IUsuarioRepository _usuarioRepository;

        public LoginController(IOptions<AppSettings> options, IJwtService jwtService, IUsuarioRepository usuarioRepository)
        {
            _jwtService = jwtService;
            _appSettings = options.Value;
            _usuarioRepository = usuarioRepository;
        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromQuery] int document, [FromQuery] string contrasena)
        {

            string key = _appSettings.SecrectKey;

            Usuario usuario = await _usuarioRepository.UsuarioByDocumentAndPassword(document, contrasena);
            if(usuario is null)
            {
                return BadRequest(new
                {
                    Message = "El usuario o el password no coinciden o el documento no existe"
                });
            }
            string token = _jwtService.GenerateToken(key, usuario.Documento, usuario.Email, usuario.TipoUsuario.Nombres, usuario.Nombres);

            return Ok(new
            {
                token
            });
        }

    }
}
