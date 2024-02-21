using dciSphere.Abstraction.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dciSphere.Abstraction.Domain;
    public abstract class Entity : IEntity
    {
        [Key] public virtual int Id { get; }
        protected Entity() { }
        protected Entity(int Id) { }
    }
