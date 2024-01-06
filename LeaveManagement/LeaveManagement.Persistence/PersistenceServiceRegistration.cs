namespace LeaveManagement.Persistence;

using LeaveManagement.Application.Contracts.Persistence;
using LeaveManagement.Persistence.DatabaseContext;
using LeaveManagement.Persistence.DatabaseContext.Repositories;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(
        this IServiceCollection services,
        IConfiguration configuration)
        => services
            .AddDbContext<LeaveManagementDatabaseContext>(options => options
                .UseSqlServer(configuration.GetConnectionString("LeaveManagementDatabaseConnectionString")))
            .AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>))
            .AddScoped<ILeaveTypeRepository, LeaveTypeRepository>()
            .AddScoped<ILeaveAllocationRepository, LeaveAllocationRepository>()
            .AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
}