using PcAsCloud.CORE.Entities;
using System.Linq.Expressions;

namespace PcAsCloud.CORE.RepositoryInstances;

public interface IGenericRepository<T> where T : BaseEntity, new()
{
    Task<IEnumerable<T>> GetAllAsync(params string[]? includes);
    Task<T?> GetByIdAsync(string id, Expression<Func<T, bool>>? where = null, params string[]? includes);
    Task<T> CreateAsync(T entity);
    Task DeleteAsync(T entity);
    Task SaveChangesAsync();
}