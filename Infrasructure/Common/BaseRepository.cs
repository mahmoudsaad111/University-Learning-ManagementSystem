using Application.Common.Interfaces.Presistance;
using InfraStructure;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Common
{
	public class BaseRepository<T> : IBaseRepository<T> where T : class
	{
		protected AppDbContext _appDbContext;

		public BaseRepository(AppDbContext _appDbContext)
		{
			this._appDbContext = _appDbContext;
		}

		public async Task<T> CreateAsync(T entity)
		{
			if (entity is null)
				return null;
			await _appDbContext.Set<T>().AddAsync(entity);
			return entity ;
		}

		public async Task<bool> DeleteAsync(int? Id)
		{
			if (Id is null)
				return false;
			var objToDelete = await GetByIdAsync(Id);
			if (objToDelete is not null)
			{
				_appDbContext.Set<T>().Remove(objToDelete);
				return true;
			}
			return false;
		}

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			try
			{
				return await _appDbContext.Set<T>().ToListAsync();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
			return null;
		}

		public async Task<T> GetByIdAsync(int? Id)
		{
			if (Id is null)
				return null;
			return await _appDbContext.Set<T>().FindAsync(Id);
		}

		public async Task<bool> UpdateAsync(T? entity)
		{
			if (entity is null  ) 
				return false;
			_appDbContext.Set<T>().Update(entity);
			return true;
		}
		public T Create(T entity)
		{
			if (entity is null)
				return null;
			_appDbContext.Set<T>().Add(entity);		 
			return entity;
		}
		public bool Update(int? Id, T entity)
		{
			if (Id is null || entity is null)
				return false;

			return true;
		}
		public T GetById(int? Id)
		{
			if (Id is null)
				return null;
			return _appDbContext.Set<T>().Find(Id);
		}

		public IEnumerable<T> GetAll()
		{
			return _appDbContext.Set<T>().ToList();
		}
		public bool Delete(int? Id)
		{
			if (Id is null)
				return false;
			var objToDelete = GetById(Id);
			if (objToDelete is not null)
			{
				_appDbContext.Set<T>().Remove(objToDelete);
				return true;
			}
			return false;
		}
		public T Find(Expression<Func<T, bool>> criteria, string[] includes = null)
		{
			IQueryable<T> query = _appDbContext.Set<T>();

			if (includes != null)
				foreach (var incluse in includes)
					query = query.Include(incluse);

			return query.SingleOrDefault(criteria);
		}

		public async Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null)
		{
			IQueryable<T> query = _appDbContext.Set<T>();

			if (includes != null)
				foreach (var incluse in includes)
					query = query.Include(incluse);

			return await query.SingleOrDefaultAsync(criteria);
		}
		public async Task<IEnumerable<T>> FindAllAsyncInclude(string[] includes = null)
		{
			IQueryable<T> query = _appDbContext.Set<T>();
			if(includes!= null)
                foreach (var item in includes)
					query=query.Include(item);
                
			return await query.ToListAsync();
        }

		public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, string[] includes = null)
		{
			IQueryable<T> query = _appDbContext.Set<T>();

			if (includes != null)
				foreach (var include in includes)
					query = query.Include(include);

			return query.Where(criteria).ToList();
		}

		public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, int skip, int take)
		{
			return _appDbContext.Set<T>().Where(criteria).Skip(skip).Take(take).ToList();
		}
		/*
		public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, int? skip, int? take,
			Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending)
		{
			IQueryable<T> query = Context.Set<T>().Where(criteria);

			if (skip.HasValue)
				query = query.Skip(skip.Value);

			if (take.HasValue)
				query = query.Take(take.Value);

			if (orderBy != null)
			{
				if (orderByDirection == OrderBy.Ascending)
					query = query.OrderBy(orderBy);
				else
					query = query.OrderByDescending(orderBy);
			}

			return query.ToList();
		}
		*/
		public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, string[] includes = null)
		{
			IQueryable<T> query = _appDbContext.Set<T>();
			if (includes != null)
				foreach (var include in includes)
					query = query.Include(include);

			return await query.Where(criteria).ToListAsync();
		}

		public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int take, int skip)
		{
			return await _appDbContext.Set<T>().Where(criteria).Skip(skip).Take(take).ToListAsync();
		}

 
		/*
public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int? take, int? skip,
Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending)
{
IQueryable<T> query = Context.Set<T>().Where(criteria);

if (take.HasValue)
query = query.Take(take.Value);

if (skip.HasValue)
query = query.Skip(skip.Value);

if (orderBy != null)
{
if (orderByDirection == OrderBy.Ascending)
query = query.OrderBy(orderBy);
else
query = query.OrderByDescending(orderBy);
}

return await query.ToListAsync();
}
*/
	}
}
