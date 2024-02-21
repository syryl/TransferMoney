using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace dciSphere.Abstractions.Repository;
public interface IViewRepository<TEntity> where TEntity : class
{
    IQueryable<TEntity> View { get; }
}