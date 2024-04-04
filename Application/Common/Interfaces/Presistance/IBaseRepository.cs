using System.Linq.Expressions;

namespace Application.Common.Interfaces.Presistance
{
    public interface IBaseRepository<T> : ICRUD<T>, ICRUDAsync<T> where T : class
    {
        T Find(Expression<Func<T, bool>> criteria, string[] includes = null);
        Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null);
        public Task<IEnumerable<T>> FindAllAsyncInclude(string[] includes = null);
		IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, string[] includes = null);
        IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, int take, int skip);
        //	IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, int? take, int? skip,
        //	Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending);

        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, string[] includes = null);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int skip, int take);
        //	Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int? skip, int? take,
        //	Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending);
     
    }
}
