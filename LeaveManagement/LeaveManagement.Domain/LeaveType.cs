namespace LeaveManagement.Domain;

public class LeaveType
{
    public int Id { get; init; }

    public string Name { get; set; } = string.Empty;

    public int DefaultDays { get; set; }
}
