namespace LeaveManagement.Application.Contracts.Persistence;

using LeaveManagement.Domain;

public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
{
    Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id);

    Task<IEnumerable<LeaveAllocation>> GetLeaveAllocationsWithDetails();

    Task<IEnumerable<LeaveAllocation>> GetLeaveAllocationsWithDetails(string userId);

    Task<bool> AllocationExists(string userId, int leaveTypeId, int period);

    Task AddAllocations(IEnumerable<LeaveAllocation> allocations);

    Task<LeaveAllocation> GetUserAllocations(string userId, int leaveTypeId);
}
