namespace LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;

using LeaveManagement.Application.Contracts.Persistence;
using LeaveManagement.Application.Features.LeaveRequest.Shared;

using FluentValidation;

public class CreateLeaveRequestCommandValidator
    : AbstractValidator<CreateLeaveRequestCommand>
{
    private readonly ILeaveTypeRepository leaveTypeRepository;

    public CreateLeaveRequestCommandValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        this.leaveTypeRepository = leaveTypeRepository;

        Include(new BaseLeaveRequestValidator(this.leaveTypeRepository));
    }
}
