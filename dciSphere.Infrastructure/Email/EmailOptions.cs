using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dciSphere.Infrastructure.Email;
public class EmailOptions
{
    public string FromEmail { get; set; }
    public string FromName {  get; set; }
    public SendGridOptions SendGrid { get; set; }
    public class SendGridOptions
    {
        public string ApiKey { get; set; }
    }
}
