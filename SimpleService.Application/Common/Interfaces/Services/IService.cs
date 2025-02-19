namespace SimpleService.Application.Common.Interfaces.Services;
public interface IService<T>
{
  Task<IEnumerable<T>> GetAllAsync();
  Task<T> GetByIdAsync(Guid id);
  Task<T> AddAsync(T entity);
  Task<T> UpdateAsync(T entity);
}