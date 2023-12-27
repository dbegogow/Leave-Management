namespace LeaveManagement.Persistence.DatabaseContext.Repositories;

using LeaveManagement.Application.Contracts.Persistence;
using LeaveManagement.Domain;

public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
{
    public LeaveRequestRepository(HrDatabaseContext context)
        : base(context)
    {
    }
}
