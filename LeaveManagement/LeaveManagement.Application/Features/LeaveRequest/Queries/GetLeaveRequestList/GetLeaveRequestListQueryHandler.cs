namespace LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;

using System.Threading;
using System.Threading.Tasks;

using LeaveManagement.Application.Contracts.Persistence;

using AutoMapper;
using MediatR;

public class GetLeaveRequestListQueryHandler : IRequestHandler<GetLeaveRequestListQuery, IEnumerable<LeaveRequestListDto>>
{
    private readonly ILeaveRequestRepository leaveRequestRepository;
    private readonly IMapper mapper;

    public GetLeaveRequestListQueryHandler(
        ILeaveRequestRepository leaveRequestRepository,
        IMapper mapper)
    {
        this.leaveRequestRepository = leaveRequestRepository;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<LeaveRequestListDto>> Handle(
        GetLeaveRequestListQuery request,
        CancellationToken cancellationToken)
    {
        var leaveRequests = await this.leaveRequestRepository.GetLeaveRequestsWithDetails();

        var requests = this.mapper.Map<IEnumerable<LeaveRequestListDto>>(leaveRequests);

        return requests;
    }
}
