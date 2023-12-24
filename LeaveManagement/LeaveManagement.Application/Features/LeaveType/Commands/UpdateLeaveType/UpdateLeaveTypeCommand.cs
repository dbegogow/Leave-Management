namespace LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;

using MediatR;

public record UpdateLeaveTypeCommand(
    string Name,
    int DefaultDays)
    : IRequest<Unit>;
