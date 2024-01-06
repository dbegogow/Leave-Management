namespace LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;

using LeaveManagement.Application.Contracts.Persistence;
using LeaveManagement.Application.Exceptions;

using AutoMapper;
using MediatR;

public class UpdateLeaveAllocationCommandHandler
    : IRequestHandler<UpdateLeaveAllocationCommand, Unit>
{
    private readonly ILeaveAllocationRepository leaveAllocationRepository;
    private readonly ILeaveTypeRepository leaveTypeRepository;
    private readonly IMapper mapper;

    public UpdateLeaveAllocationCommandHandler(
        ILeaveAllocationRepository leaveAllocationRepository,
        ILeaveTypeRepository leaveTypeRepository,
        IMapper mapper)
    {
        this.leaveAllocationRepository = leaveAllocationRepository;
        this.leaveTypeRepository = leaveTypeRepository;
        this.mapper = mapper;
    }

    public async Task<Unit> Handle(
        UpdateLeaveAllocationCommand request,
        CancellationToken cancellationToken)
    {
        var validator = new UpdateLeaveAllocationCommandValidator(this.leaveAllocationRepository, this.leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            throw new BadRequestException("Invalid Leave Allocation", validationResult);
        }

        var leaveAllocation = await this.leaveAllocationRepository.GetByIdAsync(request.Id);

        if (leaveAllocation == null)
        {
            throw new NotFoundException(nameof(LeaveAllocation), request.Id);
        }

        this.mapper.Map(request, leaveAllocation);

        await this.leaveAllocationRepository.UpdateAsync(leaveAllocation);

        return Unit.Value;
    }
}
