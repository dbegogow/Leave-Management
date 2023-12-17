namespace LeaveManagement.Domain;

public class LeaveAllocation
{
    public int Id { get; init; }

    public int NumberOfDays { get; set; }

    public LeaveType LeaveType { get; set; }

    public int LeaveTypeId { get; set; }

    public int Period { get; set; }
}
