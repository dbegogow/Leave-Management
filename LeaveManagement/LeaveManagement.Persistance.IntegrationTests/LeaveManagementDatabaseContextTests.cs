namespace LeaveManagement.Persistance.IntegrationTests;

using LeaveManagement.Domain;
using LeaveManagement.Persistence.DatabaseContext;

using Microsoft.EntityFrameworkCore;

using Shouldly;

public class LeaveManagementDatabaseContextTests
{
    private readonly LeaveManagementDatabaseContext context;

    public LeaveManagementDatabaseContextTests()
    {
        var dbOptions = new DbContextOptionsBuilder<LeaveManagementDatabaseContext>()
            .UseInMemoryDatabase(
                Guid.NewGuid().ToString())
            .Options;

        this.context = new LeaveManagementDatabaseContext(dbOptions);
    }

    [Fact]
    public async void Save_Set_Date_Created_Value()
    {
        var leaveType = new LeaveType
        {
            Id = 1,
            DefaultDays = 10,
            Name = "Test Vacation"
        };

        await this.context.LeaveTypes.AddAsync(leaveType);
        await this.context.SaveChangesAsync();

        leaveType.DateCreated.ShouldNotBeNull();
    }

    [Fact]
    public async void Save_Set_Date_Modified_Value()
    {
        var leaveType = new LeaveType
        {
            Id = 1,
            DefaultDays = 10,
            Name = "Test Vacation"
        };

        await this.context.LeaveTypes.AddAsync(leaveType);
        await this.context.SaveChangesAsync();

        leaveType.DateModified.ShouldNotBeNull();
    }
}
