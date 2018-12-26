using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationPipeline
{
    public static class MyStaticFileExtensions
    {
        public static IApplicationBuilder UseMyStaticFile(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException("app");
            }
            return UseMiddlewareExtensions.UseMiddleware<MyStaticFileMiddleware>(app, Array.Empty<object>());
        }
    }
}
