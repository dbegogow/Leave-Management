namespace LeaveManagement.Persistence.DatabaseContext.Repositories;

using System.Threading.Tasks;

using LeaveManagement.Application.Contracts.Persistence;
using LeaveManagement.Domain;

using Microsoft.EntityFrameworkCore;

public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
{
    public LeaveTypeRepository(LeaveManagementDatabaseContext context)
        : base(context)
    {
    }

    public async Task<bool> IsLeaveTypeUnique(string name)
        => await base.context.LeaveTypes
            .AnyAsync(q => q.Name == name);
}
