namespace LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;

using MediatR;

public record GetLeaveRequestListQuery : IRequest<IEnumerable<LeaveRequestListDto>>;
