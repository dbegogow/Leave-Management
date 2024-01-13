namespace LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;

using LeaveManagement.Application.Features.LeaveRequest.Shared;

using MediatR;

public record CreateLeaveRequestCommand(
    string RequestComments,
    DateTime StartDate,
    DateTime EndDate,
    int LeaveTypeId)
    : BaseLeaveRequest(StartDate, EndDate, LeaveTypeId), IRequest<Unit>;
