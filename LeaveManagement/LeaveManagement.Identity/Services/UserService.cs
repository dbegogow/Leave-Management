namespace LeaveManagement.Identity.Services;

using LeaveManagement.Application.Contracts.Identity;
using LeaveManagement.Application.Models.Identity;
using LeaveManagement.Identity.Models;

using Microsoft.AspNetCore.Identity;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> userManager;

    public UserService(UserManager<ApplicationUser> userManager)
        => this.userManager = userManager;

    public async Task<Employee> GetEmployee(string userId)
    {
        var employee = await this.userManager.FindByIdAsync(userId);

        return new Employee
        {
            Email = employee.Email,
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName
        };
    }

    public async Task<IEnumerable<Employee>> GetEmployees()
    {
        var employees = await this.userManager.GetUsersInRoleAsync("Employee");

        return employees
            .Select(q => new Employee
            {
                Id = q.Id,
                Email = q.Email,
                FirstName = q.FirstName,
                LastName = q.LastName
            })
            .ToList();
    }
}
