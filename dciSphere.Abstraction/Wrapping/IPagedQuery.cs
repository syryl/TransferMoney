using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dciSphere.Abstraction.Wrapping;
public interface IPagedQuery
{
    int Page { get; set; }
    int Size { get; set; }
}
