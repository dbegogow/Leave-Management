namespace LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;

using MediatR;

public record GetLeaveTypeDetailsQuery(int Id) : IRequest<LeaveTypeDetailsDto>;
