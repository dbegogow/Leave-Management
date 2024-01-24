namespace LeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval;

using MediatR;

public record ChangeLeaveRequestApprovalCommand(int Id, bool Approved) : IRequest<Unit>;
