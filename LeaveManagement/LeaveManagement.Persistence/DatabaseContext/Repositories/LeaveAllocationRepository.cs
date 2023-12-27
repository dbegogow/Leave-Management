namespace LeaveManagement.Persistence.DatabaseContext.Repositories;

using LeaveManagement.Application.Contracts.Persistence;
using LeaveManagement.Domain;

public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
{
    public LeaveAllocationRepository(HrDatabaseContext context) : base(context)
    {
    }
}
