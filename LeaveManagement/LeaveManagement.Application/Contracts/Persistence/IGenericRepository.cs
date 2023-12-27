namespace LeaveManagement.Application.Contracts.Persistence;

using LeaveManagement.Domain.Common;

public interface IGenericRepository<T>
    where T : BaseEntity
{
    Task<IReadOnlyCollection<T>> GetAsync();

    Task<T> GetByIdAsync(int id);

    Task CreateAsync(T entity);

    Task UpdateAsync(T entity);

    Task DeleteAsync(T entity);
}
