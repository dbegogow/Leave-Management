namespace LeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval;

using FluentValidation;

public class ChangeLeaveRequestApprovalCommandValidator
    : AbstractValidator<ChangeLeaveRequestApprovalCommand>
{
    public ChangeLeaveRequestApprovalCommandValidator()
    {
        RuleFor(p => p.Approved)
            .NotNull()
            .WithMessage("Approval status cannot be null");
    }
}
