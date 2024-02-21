using dciSphere.Abstraction.Auth;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace dciSphere.Infrastructure.Auth;
public sealed class AuthManager(AuthOptions authOptions) : IAuthManager
{
    public JwtDto GenerateAccessToken(object userId, string username, TimeSpan expiration, string[] permissions, string role = null)
    {
        var keyBytes = Encoding.UTF8.GetBytes(authOptions.SigningKey);
        var symmetricKey = new SymmetricSecurityKey(keyBytes);

        var signingCredentials = new SigningCredentials(
            symmetricKey,
            SecurityAlgorithms.HmacSha256);
        var now = DateTime.UtcNow;

        var claims = new List<Claim>()
        {
            new(JwtRegisteredClaimNames.Sub, username),
            new(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUnixTimeMilliseconds().ToString())
        };

        var roleClaims = permissions.Select(x => new Claim("role", x));
        claims.AddRange(roleClaims);
        var expires = DateTime.Now.Add(expiration);
        var token = new JwtSecurityToken(
            issuer: authOptions.Issuer,
            audience: authOptions.Audience,
            claims: claims,
            expires: expires,
            signingCredentials: signingCredentials);

        var rawToken = new JwtSecurityTokenHandler().WriteToken(token);
        return new JwtDto(rawToken, expires, userId, username);
    }
}
