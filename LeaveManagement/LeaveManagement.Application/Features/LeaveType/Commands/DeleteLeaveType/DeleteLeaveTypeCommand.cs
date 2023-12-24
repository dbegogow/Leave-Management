namespace LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;

using MediatR;

public record DeleteLeaveTypeCommand(
    int Id)
    : IRequest<Unit>;