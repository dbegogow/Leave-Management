namespace LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;

using LeaveManagement.Application.Contracts.Persistence;
using LeaveManagement.Domain;

using AutoMapper;
using MediatR;

public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
{
    private readonly ILeaveTypeRepository leaveTypeRepository;
    private readonly IMapper mapper;

    public UpdateLeaveTypeCommandHandler(
        ILeaveTypeRepository leaveTypeRepository,
        IMapper mapper)
    {
        this.leaveTypeRepository = leaveTypeRepository;
        this.mapper = mapper;
    }

    public async Task<Unit> Handle(
        UpdateLeaveTypeCommand request,
        CancellationToken cancellationToken)
    {
        var leaveTypeToUpdate = this.mapper.Map<LeaveType>(request);

        await this.leaveTypeRepository.UpdateAsync(leaveTypeToUpdate);

        return Unit.Value;
    }
}
