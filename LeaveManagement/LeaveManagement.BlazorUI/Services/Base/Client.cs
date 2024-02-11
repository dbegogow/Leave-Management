namespace LeaveManagement.BlazorUI.Services.Base;

public partial class Client : IClient
{
    public HttpClient HttpClient => this._httpClient;
}
