namespace LeaveManagement.BlazorUI.Services;

using LeaveManagement.BlazorUI.Contracts;
using LeaveManagement.BlazorUI.Models.LeaveTypes;
using LeaveManagement.BlazorUI.Services.Base;

using AutoMapper;

public class LeaveTypeService : BaseHttpService, ILeaveTypeService
{
    private readonly IMapper mapper;

    public LeaveTypeService(
        IClient client,
        IMapper mapper)
        : base(client)
        => this.mapper = mapper;

    public async Task<IEnumerable<LeaveTypeViewModel>> GetLeaveTypes()
        => this.mapper.Map<IEnumerable<LeaveTypeViewModel>>(
            await base.client.LeaveTypesAllAsync());

    public async Task<LeaveTypeViewModel> GetLeaveTypeDetails(int id)
        => this.mapper.Map<LeaveTypeViewModel>(
            await base.client.LeaveTypesGETAsync(id));

    public async Task<Response<Guid>> CreateLeaveType(LeaveTypeViewModel leaveType)
    {
        try
        {
            var createLeaveTypeCommand = this.mapper.Map<CreateLeaveTypeCommand>(leaveType);

            await base.client.LeaveTypesPOSTAsync(createLeaveTypeCommand);

            return new Response<Guid>() { Success = true };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }

    public async Task<Response<Guid>> UpdateLeaveType(int id, LeaveTypeViewModel leaveType)
    {
        try
        {
            var updateLeaveTypeCommand = this.mapper.Map<UpdateLeaveTypeCommand>(leaveType);

            await base.client.LeaveTypesPUTAsync(id.ToString(), updateLeaveTypeCommand);

            return new Response<Guid>() { Success = true };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }

    public async Task<Response<Guid>> DeleteLeaveType(int id)
    {
        try
        {
            await base.client.LeaveTypesDELETEAsync(id);

            return new Response<Guid>() { Success = true };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }
}