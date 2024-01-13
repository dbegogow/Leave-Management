
namespace LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;

using LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

public class LeaveRequestListDto
{
    public string RequestingEmployeeId { get; set; }

    public LeaveTypeDto LeaveType { get; set; }

    public DateTime DateRequested { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public bool? Approved { get; set; }
}
