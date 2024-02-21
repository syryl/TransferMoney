using dciSphere.Abstraction.Email;
using SendGrid.Helpers.Mail;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dciSphere.Infrastructure.Email;
public sealed class SendGridSender(EmailOptions options) : IEmailSender
{
    public async Task SendAsync(string content, string to, string subject)
    {
        var apiKey = options.SendGrid.ApiKey;
        var fromEmail = options.FromEmail;
        var fromName = options.FromName;
        var client = new SendGridClient(apiKey);
        var msg = new SendGridMessage()
        {
            From = new EmailAddress(fromEmail, fromName),
            Subject = subject,
            HtmlContent = content
        };
        msg.AddTo(new EmailAddress(to));
        var response = await client.SendEmailAsync(msg);
        await Task.CompletedTask;
    }
}
