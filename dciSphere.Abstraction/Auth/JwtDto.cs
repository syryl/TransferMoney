using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dciSphere.Abstraction.Auth;
public record JwtDto(string AccessToken, DateTime Expiry, object UserId, string Role);