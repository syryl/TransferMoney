using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dciSphere.Abstraction.Email;
public interface IEmailSender
{
    Task SendAsync(string content, string to, string subject);
}
