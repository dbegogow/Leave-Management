namespace LeaveManagement.Application.Contracts.Persistence;

using LeaveManagement.Domain;

public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
{
    Task<LeaveRequest> GetLeaveRequestWithDetails(int id);

    Task<IEnumerable<LeaveRequest>> GetLeaveRequestsWithDetails();

    Task<IEnumerable<LeaveRequest>> GetLeaveRequestsWithDetails(string userId);
}
