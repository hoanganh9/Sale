using Repository.Pattern.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository.Pattern.Repositories
{
    public interface IQueryFluent<TEntity> where TEntity : IObjectState
    {
        IQueryFluent<TEntity> OrderBy(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);

        IQueryFluent<TEntity> Include(Expression<Func<TEntity, object>> expression);

        IQueryFluent<TEntity> Include(string include);

        IQueryFluent<TEntity> Include(List<string> lstinclude);

        int Count();

        IEnumerable<TEntity> SelectPage(int page, int pageSize, out int totalCount);

        IQueryable<TEntity> SelectPageQ(int page, int pageSize, out int totalCount);

        IEnumerable<TResult> Select<TResult>(Expression<Func<TEntity, TResult>> selector = null);

        IQueryable<TResult> SelectQ<TResult>(Expression<Func<TEntity, TResult>> selector = null);

        IEnumerable<TEntity> Select();

        IQueryable<TEntity> SelectQ();

        Task<IEnumerable<TEntity>> SelectAsync();

        IQueryable<TEntity> SqlQuery(string query, params object[] parameters);
    }
}