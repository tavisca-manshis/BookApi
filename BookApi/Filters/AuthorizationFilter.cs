using BookApi.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BookApi.Filters
{
    public class AuthorizationFilter : Attribute, IAuthorizationFilter
    {

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            Microsoft.Extensions.Primitives.StringValues userRole;
            context.HttpContext.Request.Headers.TryGetValue("userRole", out userRole);


            if (userRole != KeyStore.UserRole.Admin)
            {
                var message = "Unauthorized Access";
                var error = new { error = message };
                context.Result = new JsonResult(error);
                context.HttpContext.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
                Console.WriteLine(message);

            }
        }
    }
}
