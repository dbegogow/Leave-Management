namespace LeaveManagement.Application.Contracts.Email;

using LeaveManagement.Application.Models.Email;

public interface IEmailSender
{
    Task<bool> SendEmail(EmailMessage email);
}
