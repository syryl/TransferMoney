using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;

namespace dciSphere.Abstractions.Repository;
public interface IRepository<TEntity> where TEntity : class
{
    DbSet<TEntity> Entity { get; } 
    public Task CommitAsync();
}