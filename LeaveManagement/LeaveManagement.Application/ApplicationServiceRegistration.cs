namespace LeaveManagement.Application;

using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        return services
            .AddAutoMapper(assembly)
            .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));
    }
}
