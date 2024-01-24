namespace LeaveManagement.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;

using MediatR;

public record CancelLeaveRequestCommand(int Id) : IRequest<Unit>;
