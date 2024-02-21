using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dciSphere.Abstraction.Domain.Contracts;
public interface ISoftDelete
{
    public bool IsDeleted { get; }
}
