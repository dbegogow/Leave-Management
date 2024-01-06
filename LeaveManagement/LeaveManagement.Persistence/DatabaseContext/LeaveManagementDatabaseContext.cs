namespace LeaveManagement.Persistence.DatabaseContext;

using LeaveManagement.Domain;
using LeaveManagement.Domain.Common;

using Microsoft.EntityFrameworkCore;

public class LeaveManagementDatabaseContext : DbContext
{
    public LeaveManagementDatabaseContext(DbContextOptions<LeaveManagementDatabaseContext> options)
        : base(options)
    {
    }

    public DbSet<LeaveType> LeaveTypes { get; set; }

    public DbSet<LeaveAllocation> LeaveAllocations { get; set; }

    public DbSet<LeaveRequest> LeaveRequests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LeaveManagementDatabaseContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
            .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
        {
            entry.Entity.DateModified = DateTime.Now;

            if (entry.State == EntityState.Added)
            {
                entry.Entity.DateModified = DateTime.Now;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
