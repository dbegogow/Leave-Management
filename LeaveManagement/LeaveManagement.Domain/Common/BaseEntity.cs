namespace LeaveManagement.Domain.Common;

public abstract class BaseEntity
{
    public int Id { get; init; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateModified { get; set; }
}
