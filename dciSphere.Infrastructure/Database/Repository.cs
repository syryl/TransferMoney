using dciSphere.Abstractions.Repository;
using dciSphere.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dciSphere.Infrastructure.Database;
public class Repository<TEntity>(DbContext dbContext) : IRepository<TEntity> where TEntity : class
{
    public DbSet<TEntity> Entity { get; } = dbContext.Set<TEntity>();
    public async Task CommitAsync() => await dbContext.SaveChangesAsync();
}