namespace LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;

using LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

public class LeaveAllocationDto
{
    public int Id { get; set; }

    public int NumberOfDays { get; set; }

    public LeaveTypeDto LeaveType { get; set; }

    public int LeaveTypeId { get; set; }

    public int Period { get; set; }
}
