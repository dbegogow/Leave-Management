namespace LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;

using LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

public class LeaveAllocationDetailsDto
{
    public int Id { get; set; }

    public int NumberOfDays { get; set; }

    public LeaveTypeDto LeaveType { get; set; }

    public int LeaveTypeId { get; set; }

    public int Period { get; set; }
}
