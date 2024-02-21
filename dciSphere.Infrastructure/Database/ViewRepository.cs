using dciSphere.Abstractions.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dciSphere.Infrastructure.Database;
public class ViewRepository<TEntity>(DbContext dbContext) : IViewRepository<TEntity> where TEntity : class
{
    public IQueryable<TEntity> View => dbContext.Set<TEntity>().AsNoTracking();
}
