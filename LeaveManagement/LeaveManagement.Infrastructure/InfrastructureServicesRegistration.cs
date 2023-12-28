namespace LeaveManagement.Infrastructure;

using LeaveManagement.Infrastructure.EmailService;
using LeaveManagement.Infrastructure.Logging;
using LeaveManagement.Application.Contracts.Email;
using LeaveManagement.Application.Models.Email;
using LeaveManagement.Application.Logging;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class InfrastructureServicesRegistration
{
    public static IServiceCollection ConfigureInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
        => services
            .Configure<EmailSettings>(configuration.GetSection("EmailSettings"))
            .AddTransient<IEmailSender, EmailSender>()
            .AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
}
