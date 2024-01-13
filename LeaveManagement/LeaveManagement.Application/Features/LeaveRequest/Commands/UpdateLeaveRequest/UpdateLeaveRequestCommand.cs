namespace LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;

using LeaveManagement.Application.Features.LeaveRequest.Shared;

using MediatR;

public record UpdateLeaveRequestCommand(
    int Id,
    string RequestComments,
    bool Cancelled,
    DateTime StartDate,
    DateTime EndDate,
    int LeaveTypeId)
    : BaseLeaveRequest(StartDate, EndDate, LeaveTypeId), IRequest<Unit>;
