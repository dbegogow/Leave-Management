namespace LeaveManagement.Application.Contracts.Persistence;

using LeaveManagement.Domain;

public interface ILeaveTypeRepository : IGenericRepository<LeaveType>
{
    Task<bool> IsLeaveTypeUnique(string name);
}
