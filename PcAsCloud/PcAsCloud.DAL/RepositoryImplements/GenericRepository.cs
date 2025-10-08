using Microsoft.EntityFrameworkCore;
using PcAsCloud.CORE.Entities;
using PcAsCloud.CORE.RepositoryInstances;
using PcAsCloud.DAL.Context;
using System.Linq.Expressions;

namespace PcAsCloud.DAL.RepositoryImplements;

public class GenericRepository<T>(AppDbContext _context) : IGenericRepository<T> where T : BaseEntity, new()
{
    protected DbSet<T> Table = _context.Set<T>();
    public async Task<T> CreateAsync(T entity)
    {
        await Table.AddAsync(entity);
        return entity;
    }

    public async Task<IEnumerable<T>> GetAllAsync(params string[]? includes)
    {
        var quary = Table.AsQueryable();
        quary = _addIncludes(quary, includes);
        return await quary.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(string id, Expression<Func<T, bool>>? where = null, params string[]? includes)
    {
        var quary = Table.AsQueryable();
        quary = _addIncludes(quary, includes);
        quary = _addWhere(quary, where);
        return await quary.FirstOrDefaultAsync(x => x.Id.ToString() == id);
    }

    public async Task DeleteAsync(T entity)
    {
        await Task.Run(() => Table.Remove(entity));
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    IQueryable<T> _addWhere(IQueryable<T> query, Expression<Func<T, bool>>? where = null)
    {
        if (where != null)
            query = query.Where(where);
        return query;
    }
    IQueryable<T> _addIncludes(IQueryable<T> query, params string[]? includes)
    {
        if (includes != null && includes.Count() != 0)
            foreach (var include in includes)
                query = query.Include(include);
        return query;
    }
}