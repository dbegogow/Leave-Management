namespace LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails;

using MediatR;

public record GetLeaveRequestDetailsQuery(int Id) : IRequest<LeaveRequestDetailsDto>;