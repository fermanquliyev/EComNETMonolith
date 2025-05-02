using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace EComNetMonolith.Shared.Endpoints
{
    public interface IEndpoint
    {
        RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder);
    }
}
