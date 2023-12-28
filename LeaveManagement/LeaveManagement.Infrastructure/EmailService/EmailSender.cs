namespace LeaveManagement.Infrastructure.EmailService;

using System.Threading.Tasks;

using LeaveManagement.Application.Contracts.Email;
using LeaveManagement.Application.Models.Email;

using Microsoft.Extensions.Options;

using SendGrid;
using SendGrid.Helpers.Mail;

public class EmailSender : IEmailSender
{
    public EmailSender(IOptions<EmailSettings> emailSettings)
        => this.EmailSettings = emailSettings.Value;

    public EmailSettings EmailSettings { get; }

    public async Task<bool> SendEmail(EmailMessage email)
    {
        var client = new SendGridClient(this.EmailSettings.ApiKey);
        var to = new EmailAddress(email.To);
        var from = new EmailAddress
        {
            Email = this.EmailSettings.FromAddress,
            Name = this.EmailSettings.FromName
        };

        var message = MailHelper.CreateSingleEmail(from, to, email.Subject, email.Body, email.Body);

        var response = await client.SendEmailAsync(message);

        return response.IsSuccessStatusCode;
    }
}
