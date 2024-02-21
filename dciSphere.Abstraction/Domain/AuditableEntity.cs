using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dciSphere.Abstraction.Domain;
public abstract class AuditableEntity : Entity
{
    public DateTime CreatedAt { get; }
    public int? CreatedBy { get; }
    public DateTime ModifiedAt { get; }
    public int? ModifiedBy { get; }
}
