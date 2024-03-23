namespace LeaveManagement.Application.Contracts.Identity;

using LeaveManagement.Application.Models.Identity;

public interface IUserService
{
    Task<IEnumerable<Employee>> GetEmployees();

    Task<Employee> GetEmployee(string userId);
}
