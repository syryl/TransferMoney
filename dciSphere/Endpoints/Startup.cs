using System.Runtime.InteropServices.Marshalling;

namespace dciSphere.Endpoints;

public static class Startup 
{
    private const string api_version = "v1";
    public static void ExposeApiV1(this IEndpointRouteBuilder apiBuilder)
    {
        apiBuilder.MapGroup($"/{api_version}/banks").GetBankEndpoints();
    }
}
