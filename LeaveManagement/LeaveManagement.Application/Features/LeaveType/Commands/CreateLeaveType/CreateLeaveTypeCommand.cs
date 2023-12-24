namespace LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;

using MediatR;

public record CreateLeaveTypeCommand(
    string Name,
    int DefaultDays)
    : IRequest<int>;