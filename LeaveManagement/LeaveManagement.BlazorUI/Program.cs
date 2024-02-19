using System.Reflection;

using LeaveManagement.BlazorUI;
using LeaveManagement.BlazorUI.Contracts;
using LeaveManagement.BlazorUI.Services;
using LeaveManagement.BlazorUI.Services.Base;

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services
    .AddScoped<ILeaveTypeService, LeaveTypeService>()
    .AddScoped<ILeaveRequestService, ILeaveRequestService>()
    .AddScoped<ILeaveAllocationService, LeaveAllocationService>()
    .AddHttpClient<IClient, Client>(client =>
        client.BaseAddress = new Uri("https://localhost:7118"));

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

await builder
    .Build()
    .RunAsync();
