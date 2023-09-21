using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Ubiety.Dns.Core;

namespace HotelWeb.Api.Web.Util
{
    public class RolAuthorizationFilter : IAsyncAuthorizationFilter
    {
        private readonly string _requiredRole;

        public RolAuthorizationFilter(string requiredRole)
        {
            _requiredRole = requiredRole;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (!user.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (!user.IsInRole(_requiredRole))
            {
                var response = new
                {
                    Message = "No tienes permiso para acceder a esta acción."
                    // Puedes agregar más propiedades al objeto response según tus necesidades.
                };

                var jsonResult = new JsonResult(response)
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                };

                context.Result = jsonResult;
                return ;
            }
        }
    }
}
