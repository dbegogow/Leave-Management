namespace LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails;

using System.Threading;
using System.Threading.Tasks;

using LeaveManagement.Application.Contracts.Persistence;

using AutoMapper;
using MediatR;

public class GetLeaveRequestDetailsQueryHandler : IRequestHandler<GetLeaveRequestDetailsQuery, LeaveRequestDetailsDto>
{
    private readonly ILeaveRequestRepository leaveRequestRepository;
    private readonly IMapper mapper;

    public GetLeaveRequestDetailsQueryHandler(
        ILeaveRequestRepository leaveRequestRepository,
        IMapper mapper)
    {
        this.leaveRequestRepository = leaveRequestRepository;
        this.mapper = mapper;
    }

    public async Task<LeaveRequestDetailsDto> Handle(GetLeaveRequestDetailsQuery request, CancellationToken cancellationToken)
    {
        var leaveRequestDetails = await this.leaveRequestRepository.GetLeaveRequestWithDetails(request.Id);

        var leaveRequest = this.mapper.Map<LeaveRequestDetailsDto>(leaveRequestDetails);

        return leaveRequest;
    }
}
