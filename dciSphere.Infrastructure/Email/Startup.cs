using dciSphere.Abstraction.Email;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dciSphere.Infrastructure.Email;
public static class Startup
{
    public static void AddEmailing(this IServiceCollection services, IConfiguration configuration, EmailProvider provider)
    {
        services.BindSettings<EmailOptions>(configuration, "email");
        if (provider == EmailProvider.SendGrid)
            services.AddSingleton<IEmailSender, SendGridSender>();
    }
}
