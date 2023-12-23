namespace LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

using MediatR;

public record GetLeaveTypesQuery : IRequest<IEnumerable<LeaveTypeDto>>;
