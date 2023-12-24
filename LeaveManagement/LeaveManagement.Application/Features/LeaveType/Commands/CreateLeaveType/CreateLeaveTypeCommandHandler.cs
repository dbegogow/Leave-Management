﻿namespace LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;

using System.Threading;
using System.Threading.Tasks;

using LeaveManagement.Application.Contracts.Persistence;
using LeaveManagement.Domain;

using AutoMapper;
using MediatR;

public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, int>
{
    private readonly ILeaveTypeRepository leaveTypeRepository;
    private readonly IMapper mapper;

    public CreateLeaveTypeCommandHandler(
        ILeaveTypeRepository leaveTypeRepository,
        IMapper mapper)
    {
        this.leaveTypeRepository = leaveTypeRepository;
        this.mapper = mapper;
    }

    public async Task<int> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        var leaveTypeToCreate = this.mapper.Map<LeaveType>(request);

        await this.leaveTypeRepository.CreateAsync(leaveTypeToCreate);

        return leaveTypeToCreate.Id;
    }
}
