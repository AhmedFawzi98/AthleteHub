using Resturants.Domain.Enums;
using System.Linq.Expressions;

namespace AthleteHub.Domain.Interfaces.Repositories;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> searchCritrea = null, IEnumerable<string> includes = null);
    Task<(IEnumerable<T>,int)> GetAllAsync(int pageSize, int pageNumber, SortingDirection sortingDirection,
        Expression<Func<T, object>> sortingCritrea = null, IEnumerable<Expression<Func<T, bool>>> filtersCritrea = null
       , Expression < Func<T, bool>> searchCritrea = null, Dictionary<Expression<Func<T, object>>, KeyValuePair<Expression<Func<object, object>>, Expression<Func<object, object>>>> includes = null);
   
    Task<T> FindAsync(Expression<Func<T,bool>> criteria, Dictionary<Expression<Func<T, object>>, KeyValuePair<Expression<Func<object, object>>, Expression<Func<object, object>>>> includes = null);
    
    Task AddAsync(T entity);
   
    void Delete(T entity);
  
    void DeleteRange(IEnumerable<T> entities);
  
    void Update(T entity);
}
