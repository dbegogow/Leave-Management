namespace LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;

using LeaveManagement.Application.Contracts.Persistence;
using LeaveManagement.Application.Exceptions;
using LeaveManagement.Application.Logging;
using LeaveManagement.Domain;

using AutoMapper;
using MediatR;

public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
{
    private readonly ILeaveTypeRepository leaveTypeRepository;
    private readonly IMapper mapper;
    private readonly IAppLogger<UpdateLeaveTypeCommandHandler> logger;

    public UpdateLeaveTypeCommandHandler(
        ILeaveTypeRepository leaveTypeRepository,
        IMapper mapper,
        IAppLogger<UpdateLeaveTypeCommandHandler> logger)
    {
        this.leaveTypeRepository = leaveTypeRepository;
        this.mapper = mapper;
        this.logger = logger;
    }

    public async Task<Unit> Handle(
        UpdateLeaveTypeCommand request,
        CancellationToken cancellationToken)
    {
        var validator = new UpdateLeaveTypeCommandValidator(this.leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            this.logger.LogWarning(
                "Validation errors in update request for {0} - {1}",
                nameof(LeaveType),
                request.Id);

            throw new BadRequestException("Invalid Leave type", validationResult);
        }

        var leaveTypeToUpdate = this.mapper.Map<LeaveType>(request);

        await this.leaveTypeRepository.UpdateAsync(leaveTypeToUpdate);

        return Unit.Value;
    }
}
