namespace LeaveManagement.Persistence.DatabaseContext.Repositories;

using System.Collections.Generic;
using System.Threading.Tasks;

using LeaveManagement.Application.Contracts.Persistence;
using LeaveManagement.Domain;

using Microsoft.EntityFrameworkCore;

public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
{
    public LeaveRequestRepository(LeaveManagementDatabaseContext context)
        : base(context)
    {
    }

    public async Task<IEnumerable<LeaveRequest>> GetLeaveRequestsWithDetails()
        => await base.context.LeaveRequests
            .Include(q => q.LeaveType)
            .ToListAsync();

    public async Task<IEnumerable<LeaveRequest>> GetLeaveRequestsWithDetails(string userId)
        => await base.context.LeaveRequests
            .Where(x => x.RequestingEmployeeId == userId)
            .Include(q => q.LeaveType)
            .ToListAsync();

    public async Task<LeaveRequest> GetLeaveRequestWithDetails(int id)
        => await base.context.LeaveRequests
            .Include(q => q.LeaveType)
            .FirstOrDefaultAsync(q => q.Id == id);
}
