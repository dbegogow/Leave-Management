namespace LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;

using MediatR;

public record UpdateLeaveTypeCommand(
    int Id,
    string Name,
    int DefaultDays)
    : IRequest<Unit>;
