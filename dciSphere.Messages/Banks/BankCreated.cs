using dciSphere.Abstraction.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dciSphere.Messages.Banks;
public record BankCreated(string Name) : IEvent;