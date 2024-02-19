namespace LeaveManagement.BlazorUI.Services;

using LeaveManagement.BlazorUI.Contracts;
using LeaveManagement.BlazorUI.Services.Base;

public class LeaveAllocationService : BaseHttpService, ILeaveAllocationService
{
    public LeaveAllocationService(IClient client)
        : base(client)
    {
    }
}
