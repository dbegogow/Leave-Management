namespace LeaveManagement.BlazorUI.Services;

using LeaveManagement.BlazorUI.Contracts;
using LeaveManagement.BlazorUI.Services.Base;

public class LeaveRequestService : BaseHttpService, ILeaveRequestService
{
    public LeaveRequestService(IClient client)
        : base(client)
    {
    }
}
