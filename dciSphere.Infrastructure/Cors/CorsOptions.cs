using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dciSphere.Infrastructure.Cors;
public sealed class CorsOptions
{
    public string Name { get; set; }
    public IEnumerable<string> Origins { get; set; }
    public IEnumerable<string> Methods { get; set; }
    public IEnumerable<string> Headers { get; set; }
}
