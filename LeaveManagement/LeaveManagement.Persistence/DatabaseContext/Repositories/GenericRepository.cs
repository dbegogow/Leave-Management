namespace LeaveManagement.Persistence.DatabaseContext.Repositories;

using System.Collections.Generic;
using System.Threading.Tasks;

using LeaveManagement.Application.Contracts.Persistence;
using LeaveManagement.Domain.Common;

using Microsoft.EntityFrameworkCore;

public class GenericRepository<T> : IGenericRepository<T>
    where T : BaseEntity
{
    protected readonly LeaveManagementDatabaseContext context;

    public GenericRepository(LeaveManagementDatabaseContext context)
        => this.context = context;

    public async Task<IReadOnlyCollection<T>> GetAsync()
        => await this.context.Set<T>()
            .AsNoTracking()
            .ToListAsync();

    public async Task<T> GetByIdAsync(int id)
        => await this.context.Set<T>()
            .AsNoTracking()
            .FirstOrDefaultAsync(q => q.Id == id);

    public async Task CreateAsync(T entity)
    {
        await this.context.AddAsync(entity);

        await this.context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        this.context.Entry(entity).State = EntityState.Modified;

        await this.context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        this.context.Remove(entity);

        await this.context.SaveChangesAsync();
    }
}
