using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dciSphere.Infrastructure.Cors;
public static class Startup
{
    public static void AddCorsPolicies(this IServiceCollection services, IConfiguration configuration)
    {
        var corsConfigurations = configuration.GetOptionsList<CorsOptions>("Cors");
        foreach (var corsOption in corsConfigurations)
        {
            services.AddCors(cors =>
            {
                var allowedHeaders = corsOption.Headers ?? Enumerable.Empty<string>();
                var allowedMethods = corsOption.Methods ?? Enumerable.Empty<string>();
                var allowedOrigins = corsOption.Origins ?? Enumerable.Empty<string>();
                cors.AddPolicy("cors", corsBuilder =>
                {
                    var origins = allowedOrigins.ToArray();
                    corsBuilder.WithHeaders(allowedHeaders.ToArray())
                        .WithMethods(allowedMethods.ToArray())
                        .WithOrigins(origins.ToArray());
                });
            });
        }
    }
}
