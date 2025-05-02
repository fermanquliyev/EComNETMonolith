using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EComNetMonolith.Shared.Endpoints
{
    public static class EndpointsExtensions
    {
        public static IApplicationBuilder AddEndpoints(this IApplicationBuilder app, Assembly assembly = null)
        
        {  
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                // Register all endpoints here
                var endpointTypes = (assembly ?? Assembly.GetExecutingAssembly()).GetTypes()
                    .Where(t => t.IsClass && !t.IsAbstract && typeof(IEndpoint).IsAssignableFrom(t));

                foreach (var endpointType in endpointTypes)
                {
                    var endpointInstance = (IEndpoint)Activator.CreateInstance(endpointType);

                    if (endpointInstance is null)
                    {
                        throw new InvalidOperationException($"Could not create instance of endpoint type {endpointType.Name}");
                    }

                    endpointInstance.MapEndpoint(endpoints);
                }
            });
            return app;
        }
    }
}
