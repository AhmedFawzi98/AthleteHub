using AthleteHub.Domain.Interfaces.Repositories;
using AthleteHub.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1;
using Resturants.Domain.Enums;
using System.Linq.Expressions;
using System.Threading;

namespace AthleteHub.Infrastructure.Repositories;

internal class GenericRepository<T>(AthleteHubDbContext _context) : IGenericRepository<T> where T : class
{
    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> searchCritrea = null, IEnumerable<string> includes = null)
    {
        IQueryable<T> query = _context.Set<T>();
        if (includes != null && includes.Count() > 0)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }
        if (searchCritrea != null)
            query = query.Where(searchCritrea);

        return await query.ToListAsync();
    }


    public async Task<(IEnumerable<T>, int)> GetAllAsync(int pageSize, int pageNumber, SortingDirection sortingDirection,
        Expression<Func<T, object>> sortingCritrea = null, IEnumerable<Expression<Func<T, bool>>> filtersCritrea = null
       , Expression<Func<T, bool>> searchCritrea = null, Dictionary<Expression<Func<T, object>>, KeyValuePair<Expression<Func<object, object>>, Expression<Func<object, object>>>> includes=null)
    {
        IQueryable<T> query = _context.Set<T>();
        if (includes != null && includes.Count() > 0)
        {
            foreach (var include in includes)
            {
                if (include.Value.Value != null)
                    query = query.Include(include.Key).ThenInclude(include.Value.Key).ThenInclude(include.Value.Value);
                else if (include.Value.Key != null)
                    query = query.Include(include.Key).ThenInclude(include.Value.Key);
                else
                    query = query.Include(include.Key);
            }
        }

        
        if (searchCritrea != null)
            query = query.Where(searchCritrea);

        if(filtersCritrea != null)
        {
            foreach(var filter in filtersCritrea)
                query = query.Where(filter);
        }

        int totalCount = await query.CountAsync();


        if (sortingCritrea != null)
        {
            query = sortingDirection == SortingDirection.Ascending ?
                query.OrderBy(sortingCritrea) : query.OrderByDescending(sortingCritrea);
        }

        query = query.Skip(pageSize * (pageNumber - 1))
            .Take(pageSize);

        return (await query.ToListAsync(), totalCount);
    }

    public async Task<T> FindAsync(Expression<Func<T, bool>> criteria, Dictionary<Expression<Func<T, object>>, KeyValuePair<Expression<Func<object, object>>, Expression<Func<object, object>>>> includes = null)
    {
        IQueryable<T> query = _context.Set<T>();
        if (includes != null && includes.Count() > 0)
        {
            foreach (var include in includes)
            {
                if (include.Value.Value != null)
                    query = query.Include(include.Key).ThenInclude(include.Value.Key).ThenInclude(include.Value.Value);
                else if (include.Value.Key != null)
                    query = query.Include(include.Key).ThenInclude(include.Value.Key);
                else
                    query = query.Include(include.Key);
            }
        }
        return await query.FirstOrDefaultAsync(criteria);
    }
    public async Task<TResult> FindAsync<TResult>(Expression<Func<T, bool>> criteria, Expression<Func<T, TResult>> selector)
    {
        IQueryable<T> query = _context.Set<T>();
       
        query = query.Where(criteria);

        return await query.Select(selector).FirstOrDefaultAsync();
    }

    public async Task<T> FindAsync(
    Expression<Func<T, bool>> criteria,
    SortingDirection sortingDirection = SortingDirection.Ascending,
    Expression<Func<T, object>> sortingCritrea = null,
    string[] includes = null)
    {
        IQueryable<T> query = _context.Set<T>();

        if (includes != null && includes.Length > 0)
        {
            foreach (string include in includes)
                query = query.Include(include);
        }

        if (sortingCritrea != null)
        {
            query = sortingDirection == SortingDirection.Ascending
                ? query.OrderBy(sortingCritrea)
                : query.OrderByDescending(sortingCritrea);
        }

        return await query.FirstOrDefaultAsync(criteria);
    }

    public async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public void DeleteRange(IEnumerable<T> entities)
    {
        _context.Set<T>().RemoveRange(entities);
    }


    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }

    public async Task<int> CountAsync()
    {
        return await _context.Set<T>().CountAsync();
    }
}
