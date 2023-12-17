namespace LeaveManagement.Domain;

using LeaveManagement.Domain.Common;

public class LeaveAllocation : BaseEntity
{
    public int NumberOfDays { get; set; }

    public LeaveType? LeaveType { get; set; }

    public int LeaveTypeId { get; set; }

    public int Period { get; set; }
}
