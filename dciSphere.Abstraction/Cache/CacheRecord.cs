using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dciSphere.Abstraction.Cache;
public class CacheRecord(string prefix,
    int expiration,
    string? key = null
    )
{
    public string Prefix { get; init; } = prefix;
    public TimeSpan Expiration { get; init; } = TimeSpan.FromMinutes(expiration);
    public string? Postfix { get; private set; } = key ?? default;
    public void SetPostfix(string key) => Postfix = key;
    public string GetKey() => Postfix is not null ? $"{Prefix}-{Postfix}" : Prefix;
}