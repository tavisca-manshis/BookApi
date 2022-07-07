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
    public class ExceptionFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var error = new { error = context.Exception.Message };
            context.Result = new JsonResult(error);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            Console.WriteLine(context.Exception.Message);
        }
    }
}
