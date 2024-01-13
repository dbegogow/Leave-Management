namespace LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;

using LeaveManagement.Application.Contracts.Persistence;
using LeaveManagement.Application.Features.LeaveRequest.Shared;

using FluentValidation;

public class UpdateLeaveRequestCommandValidator
    : AbstractValidator<UpdateLeaveRequestCommand>
{
    private readonly ILeaveTypeRepository leaveTypeRepository;
    private readonly ILeaveRequestRepository leaveRequestRepository;

    public UpdateLeaveRequestCommandValidator(
        ILeaveTypeRepository leaveTypeRepository,
        ILeaveRequestRepository leaveRequestRepository)
    {
        this.leaveTypeRepository = leaveTypeRepository;
        this.leaveRequestRepository = leaveRequestRepository;

        Include(new BaseLeaveRequestValidator(this.leaveTypeRepository));

        RuleFor(p => p.Id)
            .NotNull()
            .MustAsync(LeaveRequestMustExist)
            .WithMessage("{PropertyName} must be present");
    }

    private async Task<bool> LeaveRequestMustExist(int id, CancellationToken cancellationToken)
        => await this.leaveRequestRepository.GetByIdAsync(id) != null;
}
