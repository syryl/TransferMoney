using dciSphere.Abstraction.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dciSphere.Infrastructure.Auth;
public static class Startup
{
    public static void AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var options = services.BindSettings<AuthOptions>(configuration, "Auth");
        services.AddScoped<IAuthManager, AuthManager>();

        byte[] signingKeyBytes = Encoding.UTF8.GetBytes(options.SigningKey);
        var expirationTime = DateTime.UtcNow.AddMinutes(options.ExpirationMinutes);
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opts =>
            {
                byte[] signingKeyBytes = Encoding.UTF8.GetBytes(options.SigningKey);

                opts.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = options.Issuer,
                    ValidAudience = options.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes),
                    ClockSkew = TimeSpan.Zero
                };
            });
        services.AddAuthorization(cfg =>
        {
            cfg.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                                .RequireAuthenticatedUser().Build();
        });
    }
}
