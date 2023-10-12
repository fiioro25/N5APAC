using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PAC.WebAPI.Filters
{
    public class AuthorizationFilter : Attribute, IAuthorizationFilter
    {
        public virtual void OnAuthorization(AuthorizationFilterContext context)
        {
            var authorizationHeader = context.HttpContext.Request.Headers["Authorization"].ToString();

            // Verificar si el encabezado de autorización está presente
            if (string.IsNullOrWhiteSpace(authorizationHeader))
            {
                context.Result = new UnauthorizedObjectResult("No puedo autorizarme");
            }
        }
    }
}
