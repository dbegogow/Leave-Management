using LeaveManagement.Api.Middlewares;
using LeaveManagement.Application;
using LeaveManagement.Infrastructure;
using LeaveManagement.Persistence;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(configuration)
    .AddPersistenceServices(configuration)
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddCors(options =>
        options.AddPolicy(
            "all",
            builder => builder.
                AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()))
    .AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger()
       .UseSwaggerUI();
}

app
    .UseHttpsRedirection()
    .UseAuthorization()
    .UseCors("all");

app.MapControllers();

app.UseMiddleware<ExceptionMiddleware>();

app.Run();
