namespace LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

using AutoMapper;
using LeaveManagement.Application.Contracts.Persistence;
using LeaveManagement.Application.Exceptions;
using MediatR;

public class CreateLeaveAllocationCommandHandler
    : IRequestHandler<CreateLeaveAllocationCommand, Unit>
{
    private readonly ILeaveAllocationRepository leaveAllocationRepository;
    private readonly ILeaveTypeRepository leaveTypeRepository;
    private readonly IMapper mapper;

    public CreateLeaveAllocationCommandHandler(
        ILeaveAllocationRepository leaveAllocationRepository,
        ILeaveTypeRepository leaveTypeRepository,
        IMapper mapper)
    {
        this.leaveAllocationRepository = leaveAllocationRepository;
        this.leaveTypeRepository = leaveTypeRepository;
        this.mapper = mapper;
    }

    public async Task<Unit> Handle(
        CreateLeaveAllocationCommand request,
        CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveAllocationCommandValidator(this.leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            throw new BadRequestException("Invalid Leave Allocation Request", validationResult);
        }

        var leaveAllocation = this.mapper.Map<Domain.LeaveAllocation>(request);

        await this.leaveAllocationRepository.CreateAsync(leaveAllocation);

        return Unit.Value;
    }
}
