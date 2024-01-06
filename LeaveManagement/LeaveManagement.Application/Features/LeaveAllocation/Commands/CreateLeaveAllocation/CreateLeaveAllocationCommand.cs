namespace LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

using MediatR;

public record CreateLeaveAllocationCommand(int LeaveTypeId) : IRequest<Unit>;
