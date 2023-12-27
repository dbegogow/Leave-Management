namespace LeaveManagement.Application.Contracts.Persistence;

public interface IGenericRepository<T>
    where T : class
{
    Task<IReadOnlyCollection<T>> GetAsync();

    Task<T> GetByIdAsync(int id);

    Task CreateAsync(T entity);

    Task UpdateAsync(T entity);

    Task DeleteAsync(T entity);
}
