namespace LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;

using System.Threading;
using System.Threading.Tasks;

using LeaveManagement.Application.Contracts.Persistence;
using LeaveManagement.Application.Exceptions;

using AutoMapper;
using MediatR;

public class GetLeaveTypeDetailsQueryHandler : IRequestHandler<GetLeaveTypeDetailsQuery, LeaveTypeDetailsDto>
{
    private readonly ILeaveTypeRepository leaveTypeRepository;
    private readonly IMapper mapper;

    public GetLeaveTypeDetailsQueryHandler(
        ILeaveTypeRepository leaveTypeRepository,
        IMapper mapper)
    {
        this.leaveTypeRepository = leaveTypeRepository;
        this.mapper = mapper;
    }

    public async Task<LeaveTypeDetailsDto> Handle(
        GetLeaveTypeDetailsQuery request,
        CancellationToken cancellationToken)
    {
        var leaveType = await this.leaveTypeRepository.GetByIdAsync(request.Id);

        if (leaveType == null)
        {
            throw new NotFoundException(nameof(LeaveType), request.Id);
        }

        var data = this.mapper.Map<LeaveTypeDetailsDto>(leaveType);

        return data;
    }
}
