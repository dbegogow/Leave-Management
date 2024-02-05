namespace LeaveManagement.Application.UnitTests.Features.LeaveTypes.Queries;

using LeaveManagement.Application.Contracts.Persistence;
using LeaveManagement.Application.UnitTests.Mocks;
using LeaveManagement.Application.MappingProfiles;
using LeaveManagement.Application.Logging;
using LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

using AutoMapper;
using Moq;
using Shouldly;

public class GetLeaveTypeQueryHandlerTests
{
    private readonly Mock<ILeaveTypeRepository> mockRepo;
    private readonly Mock<IAppLogger<GetLeaveTypesQueryHandler>> mockAppLogger;
    private readonly IMapper mapper;

    public GetLeaveTypeQueryHandlerTests()
    {
        this.mockRepo = MockLeaveTypeRepository.GetMockLeaveTypeRepository();

        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<LeaveTypeProfile>();
        });

        this.mapper = mapperConfig.CreateMapper();

        this.mockAppLogger = new Mock<IAppLogger<GetLeaveTypesQueryHandler>>();
    }

    [Fact]
    public async Task Get_Leave_Types_List_Test()
    {
        var handler = new GetLeaveTypesQueryHandler(this.mockRepo.Object, this.mapper, this.mockAppLogger.Object);

        var result = await handler.Handle(new GetLeaveTypesQuery(), CancellationToken.None);

        result.ShouldBeOfType<List<LeaveTypeDto>>();

        result
            .Count()
            .ShouldBe(3);
    }
}
