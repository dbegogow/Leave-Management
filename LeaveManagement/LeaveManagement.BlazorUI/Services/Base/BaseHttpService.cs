namespace LeaveManagement.BlazorUI.Services.Base;

public class BaseHttpService
{
    protected IClient client;

    public BaseHttpService(IClient client)
        => this.client = client;
}
