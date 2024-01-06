namespace LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;

using LeaveManagement.Application.Contracts.Persistence;

using FluentValidation;

public class UpdateLeaveAllocationCommandValidator
    : AbstractValidator<UpdateLeaveAllocationCommand>
{
    private readonly ILeaveAllocationRepository leaveAllocationRepository;
    private readonly ILeaveTypeRepository leaveTypeRepository;

    public UpdateLeaveAllocationCommandValidator(
        ILeaveAllocationRepository leaveAllocationRepository,
        ILeaveTypeRepository leaveTypeRepository)
    {
        RuleFor(p => p.NumberOfDays)
            .GreaterThan(0).WithMessage("{PropertyName} must greater than {ComparisonValue}");

        RuleFor(p => p.Period)
            .GreaterThanOrEqualTo(DateTime.Now.Year).WithMessage("{PropertyName} must be after {ComparisonValue}");

        RuleFor(p => p.LeaveTypeId)
            .GreaterThan(0)
            .MustAsync(LeaveTypeMustExist)
            .WithMessage("{PropertyName} does not exist.");

        RuleFor(p => p.Id)
            .NotNull()
            .MustAsync(LeaveAllocationMustExist)
            .WithMessage("{PropertyName} must be present.");

        this.leaveTypeRepository = leaveTypeRepository;
        this.leaveAllocationRepository = leaveAllocationRepository;
    }

    private async Task<bool> LeaveTypeMustExist(
        int id,
        CancellationToken cancellationToken)
        => await this.leaveTypeRepository.GetByIdAsync(id) != null;

    private async Task<bool> LeaveAllocationMustExist(
        int id,
        CancellationToken cancellationToken)
        => await this.leaveAllocationRepository.GetByIdAsync(id) != null;
}
