namespace LeaveManagement.Domain;

using LeaveManagement.Domain.Common;

public class LeaveType : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public int DefaultDays { get; set; }
}
