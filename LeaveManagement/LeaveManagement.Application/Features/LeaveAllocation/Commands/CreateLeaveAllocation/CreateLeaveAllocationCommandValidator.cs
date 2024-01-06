namespace LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

using LeaveManagement.Application.Contracts.Persistence;

using FluentValidation;

public class CreateLeaveAllocationCommandValidator
    : AbstractValidator<CreateLeaveAllocationCommand>
{
    private readonly ILeaveTypeRepository leaveTypeRepository;

    public CreateLeaveAllocationCommandValidator(
        ILeaveTypeRepository leaveTypeRepository)
    {
        RuleFor(p => p.LeaveTypeId)
            .GreaterThan(0)
            .MustAsync(LeaveTypeMustExist)
            .WithMessage("{PropertyName} does not exist");

        this.leaveTypeRepository = leaveTypeRepository;
    }

    private async Task<bool> LeaveTypeMustExist(
        int id,
        CancellationToken cancellationToken)
        => (await this.leaveTypeRepository.GetByIdAsync(id)) != null;
}
