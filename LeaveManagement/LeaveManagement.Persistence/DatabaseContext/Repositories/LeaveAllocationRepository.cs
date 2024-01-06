namespace LeaveManagement.Persistence.DatabaseContext.Repositories;

using System.Collections.Generic;
using System.Threading.Tasks;

using LeaveManagement.Application.Contracts.Persistence;
using LeaveManagement.Domain;

using Microsoft.EntityFrameworkCore;

public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
{
    public LeaveAllocationRepository(LeaveManagementDatabaseContext context) : base(context)
    {
    }

    public async Task AddAllocations(IEnumerable<LeaveAllocation> allocations)
    {
        await base.context.AddRangeAsync(allocations);

        await this.context.SaveChangesAsync();
    }

    public async Task<bool> AllocationExists(string userId, int leaveTypeId, int period)
        => await base.context.LeaveAllocations
            .AnyAsync(q => q.EmployeeId == userId
                           && q.LeaveTypeId == leaveTypeId
                           && q.Period == period);

    public async Task<IEnumerable<LeaveAllocation>> GetLeaveAllocationsWithDetails()
        => await base.context.LeaveAllocations
            .Include(q => q.LeaveType)
            .ToListAsync();

    public async Task<IEnumerable<LeaveAllocation>> GetLeaveAllocationsWithDetails(string userId)
        => await base.context.LeaveAllocations
            .Where(q => q.EmployeeId == userId)
            .Include(q => q.LeaveType)
            .ToListAsync();

    public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
        => await base.context.LeaveAllocations
            .Include(q => q.LeaveType)
            .FirstOrDefaultAsync(q => q.Id == id);

    public async Task<LeaveAllocation> GetUserAllocations(string userId, int leaveTypeId)
        => await base.context.LeaveAllocations
            .FirstOrDefaultAsync(q => q.EmployeeId == userId
                                      && q.LeaveTypeId == leaveTypeId);
}
