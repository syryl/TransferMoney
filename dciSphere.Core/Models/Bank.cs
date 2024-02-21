using dciSphere.Abstraction.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dciSphere.Core.Models
{
    public class Bank(string name) : Entity
    {
        public string Name { get; private set; } = name;
        public virtual ICollection<Account> Accounts { get; }

        public void ChnageName(string _name) => Name = _name; 
    }
}
