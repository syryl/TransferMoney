using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace dciSphere.Abstraction.Auth;
public interface IAuthManager
{
    JwtDto GenerateAccessToken(
    object userId,
    string username,
    TimeSpan expiration,
    string[] permissions,
    string role);
}
