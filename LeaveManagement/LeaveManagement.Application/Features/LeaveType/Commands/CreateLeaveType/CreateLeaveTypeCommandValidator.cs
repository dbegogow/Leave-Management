namespace LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;

using System.Threading;
using System.Threading.Tasks;

using LeaveManagement.Application.Contracts.Persistence;

using FluentValidation;

public class CreateLeaveTypeCommandValidator
    : AbstractValidator<CreateLeaveTypeCommand>
{
    private readonly ILeaveTypeRepository leaveTypeRepository;

    public CreateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .MaximumLength(70).WithMessage("{PropertyName} must be fewer than 70 characters");

        RuleFor(p => p.DefaultDays)
            .LessThan(100).WithMessage("{PropertyName} cannot exceed 100")
            .GreaterThan(1).WithMessage("{PropertyName} cannot be less than 1");

        RuleFor(q => q)
            .MustAsync(LeaveTypeNameUnique)
            .WithMessage("Leave type already exists");

        this.leaveTypeRepository = leaveTypeRepository;
    }

    private async Task<bool> LeaveTypeNameUnique(
        CreateLeaveTypeCommand command,
        CancellationToken token)
        => await this.leaveTypeRepository.IsLeaveTypeUnique(command.Name);
}
