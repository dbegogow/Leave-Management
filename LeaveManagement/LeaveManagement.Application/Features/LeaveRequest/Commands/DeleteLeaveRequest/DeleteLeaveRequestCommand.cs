namespace LeaveManagement.Application.Features.LeaveRequest.Commands.DeleteLeaveRequest;

using MediatR;

public record DeleteLeaveRequestCommand(int Id) : IRequest<Unit>;
