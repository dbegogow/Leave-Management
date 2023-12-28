namespace LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;

using LeaveManagement.Application.Contracts.Persistence;

using FluentValidation;

public class UpdateLeaveTypeCommandValidator
    : AbstractValidator<UpdateLeaveTypeCommand>
{
    private readonly ILeaveTypeRepository leaveTypeRepository;

    public UpdateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        RuleFor(p => p.Id)
            .NotNull()
            .MustAsync(LeaveTypeMustExist);

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .MaximumLength(70).WithMessage("{PropertyName} must be fewer than 70 characters");

        RuleFor(p => p.DefaultDays)
            .GreaterThan(100).WithMessage("{PropertyName} cannot exceed 100")
            .LessThan(1).WithMessage("{PropertyName} cannot be less than 1");

        RuleFor(q => q)
            .MustAsync(LeaveTypeNameUnique)
            .WithMessage("Leave type already exists");

        this.leaveTypeRepository = leaveTypeRepository;
    }

    private async Task<bool> LeaveTypeMustExist(
        int id,
        CancellationToken cancellationToken)
        => (await this.leaveTypeRepository.GetByIdAsync(id)) != null;

    private async Task<bool> LeaveTypeNameUnique(
        UpdateLeaveTypeCommand command,
        CancellationToken cancellationToken)
        => await this.leaveTypeRepository.IsLeaveTypeUnique(command.Name);
}
