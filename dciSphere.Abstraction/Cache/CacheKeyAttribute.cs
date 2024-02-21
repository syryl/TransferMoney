using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dciSphere.Abstraction.Cache;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class CacheKey : Attribute
{
}
