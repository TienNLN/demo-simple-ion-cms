using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demo_simple_ion_cms.Middleware
{
    public static class InternalExceptionMiddlewareExtension
    {
        public static IApplicationBuilder UseInternalExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<InternalExceptionMiddleware>();
        }
    }
}
