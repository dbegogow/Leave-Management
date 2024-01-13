namespace LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;

using System.Threading;
using System.Threading.Tasks;

using LeaveManagement.Application.Contracts.Email;
using LeaveManagement.Application.Contracts.Persistence;
using LeaveManagement.Application.Exceptions;
using LeaveManagement.Application.Models.Email;
using LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;
using LeaveManagement.Application.Logging;

using AutoMapper;
using MediatR;

public class CreateLeaveRequestCommandHandler
    : IRequestHandler<CreateLeaveRequestCommand, Unit>
{
    private readonly ILeaveRequestRepository leaveRequestRepository;
    private readonly ILeaveTypeRepository leaveTypeRepository;
    private readonly IMapper mapper;
    private readonly IEmailSender emailSender;
    private readonly IAppLogger<UpdateLeaveRequestCommandHandler> appLogger;

    public CreateLeaveRequestCommandHandler(
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

    public async Task<Unit> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveRequestCommandValidator(this.leaveTypeRepository);

        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            throw new BadRequestException("Invalid Leave Request", validationResult);
        }

        var leaveRequest = this.mapper.Map<Domain.LeaveRequest>(request);

        await this.leaveRequestRepository.CreateAsync(leaveRequest);

        try
        {
            var email = new EmailMessage
            {
                To = string.Empty, // Get email from employee record
                Body = $"Your leave request for {request.StartDate:D} to {request.EndDate:D} has beed updated successfully",
                Subject = "Leave Request Submitted"
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
