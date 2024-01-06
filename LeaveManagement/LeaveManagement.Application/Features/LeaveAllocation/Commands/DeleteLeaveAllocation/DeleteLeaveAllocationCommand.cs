namespace LeaveManagement.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation;

using MediatR;

public record DeleteLeaveAllocationCommand(int Id) : IRequest<Unit>;
