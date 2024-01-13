namespace LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;

using System.Threading;
using System.Threading.Tasks;

using LeaveManagement.Application.Contracts.Email;
using LeaveManagement.Application.Contracts.Persistence;
using LeaveManagement.Application.Exceptions;
using LeaveManagement.Application.Logging;
using LeaveManagement.Application.Models.Email;

using AutoMapper;
using MediatR;

public class UpdateLeaveRequestCommandHandler
    : IRequestHandler<UpdateLeaveRequestCommand, Unit>
{
    private readonly ILeaveRequestRepository leaveRequestRepository;
    private readonly ILeaveTypeRepository leaveTypeRepository;
    private readonly IMapper mapper;
    private readonly IEmailSender emailSender;
    private readonly IAppLogger<UpdateLeaveRequestCommandHandler> appLogger;

    public UpdateLeaveRequestCommandHandler(
        ILeaveRequestRepository leaveRequestRepository,
        ILeaveTypeRepository leaveTypeRepository,
        IMapper mapper,
        IEmailSender emailSender,
        IAppLogger<UpdateLeaveRequestCommandHandler> appLogger)
    {
        this.leaveRequestRepository = leaveRequestRepository;
        this.leaveTypeRepository = leaveTypeRepository;
        this.mapper = mapper;
        this.emailSender = emailSender;
        this.appLogger = appLogger;
    }

    public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateLeaveRequestCommandValidator(
            this.leaveTypeRepository,
            this.leaveRequestRepository);

        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            throw new BadRequestException("Invalid Leave Request", validationResult);
        }

        var leaveRequest = await this.leaveRequestRepository.GetByIdAsync(request.Id);

        if (leaveRequest is null)
        {
            throw new NotFoundException(nameof(LeaveRequest), request.Id);
        }

        this.mapper.Map(request, leaveRequest);

        await this.leaveRequestRepository.UpdateAsync(leaveRequest);

        try
        {
            var email = new EmailMessage
            {
                To = string.Empty, // Get email from employee record
                Body = $"Your leave request for {request.StartDate:D} to {request.EndDate:D} has beed updated successfully",
                Subject = "Leave Request Updated"
            };

            await this.emailSender.SendEmail(email);
        }
        catch (Exception ex)
        {
            this.appLogger.LogWarning(ex.Message);
        }

        return Unit.Value;
    }
}
