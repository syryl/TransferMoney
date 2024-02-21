using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dciSphere.Interaction.Banks;
public class DeleteBank
{
    public record Command() : IRequest<bool>;
}
