using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApi.Filters
{
    public class ResourceFilter : Attribute, IResourceFilter

    {

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            Console.WriteLine("Called action method " + context.ActionDescriptor.DisplayName);
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            Console.WriteLine("Calling action method " + context.ActionDescriptor.DisplayName);
        }
    }
}
