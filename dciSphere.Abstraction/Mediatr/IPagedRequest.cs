using dciSphere.Abstraction.Wrapping;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dciSphere.Abstraction.Mediatr;
public interface IPagedRequest<T> : IRequest<T>, IPagedQuery
{
}
