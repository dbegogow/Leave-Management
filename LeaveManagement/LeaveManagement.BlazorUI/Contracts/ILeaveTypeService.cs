namespace LeaveManagement.BlazorUI.Contracts;

using LeaveManagement.BlazorUI.Models.LeaveTypes;
using LeaveManagement.BlazorUI.Services.Base;

public interface ILeaveTypeService
{
    Task<IEnumerable<LeaveTypeViewModel>> GetLeaveTypes();

    Task<LeaveTypeViewModel> GetLeaveTypeDetails(int id);

    Task<Response<Guid>> CreateLeaveType(LeaveTypeViewModel leaveType);

    Task<Response<Guid>> UpdateLeaveType(int id, LeaveTypeViewModel leaveType);

    Task<Response<Guid>> DeleteLeaveType(int id);
}