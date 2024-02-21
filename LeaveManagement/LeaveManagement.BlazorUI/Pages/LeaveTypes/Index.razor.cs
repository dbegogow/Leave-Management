namespace LeaveManagement.BlazorUI.Pages.LeaveTypes;

using LeaveManagement.BlazorUI.Contracts;
using LeaveManagement.BlazorUI.Models.LeaveTypes;

using Microsoft.AspNetCore.Components;

public partial class Index
{
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public ILeaveTypeService LeaveTypeService { get; set; }

    public IEnumerable<LeaveTypeViewModel> LeaveTypes { get; private set; }

    public string Message { get; set; } = string.Empty;

    protected void CreateLeaveType()
        => this.NavigationManager.NavigateTo("/leavetypes/create/");

    protected void AllocateLeaveType(int id)
    {
    }

    protected void EditLeaveType(int id)
        => this.NavigationManager.NavigateTo($"/leavetypes/edit/{id}");

    protected void DetailsLeaveType(int id)
        => this.NavigationManager.NavigateTo($"/leavetypes/details/{id}");

    protected async Task DeleteLeaveType(int id)
    {
        var response = await this.LeaveTypeService.DeleteLeaveType(id);

        if (response.Success)
        {
            base.StateHasChanged();
        }
        else
        {
            this.Message = response.Message;
        }
    }

    protected override async Task OnInitializedAsync()
        => this.LeaveTypes = await this.LeaveTypeService.GetLeaveTypes();
}