namespace LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;

using MediatR;

public record UpdateLeaveAllocationCommand
    (int Id,
    int NumberOfDays,
    int LeaveTypeId,
    int Period)
    : IRequest<Unit>;
