using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dciSphere.Abstraction.Cache;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class GetCached(string Prefix, int Expiration) : Attribute
{
    public CacheRecord CacheRecord { get; init; } = new(Prefix, Expiration);
}
