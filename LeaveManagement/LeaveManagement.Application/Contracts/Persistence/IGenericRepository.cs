namespace LeaveManagement.Application.Contracts.Persistence;

public interface IGenericRepository<T>
    where T : class
{
    Task<T> GetAsync();

    Task<T> GetByIdAsync(string id);

    Task<T> CreateAsync(T entity);

    Task<T> UpdateAsync(T entity);

    Task<T> DeleteAsync(T entity);
}
