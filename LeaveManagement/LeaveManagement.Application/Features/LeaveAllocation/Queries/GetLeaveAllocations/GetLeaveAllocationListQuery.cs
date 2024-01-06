namespace LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;

using MediatR;

public record GetLeaveAllocationListQuery : IRequest<IEnumerable<LeaveAllocationDto>>;
