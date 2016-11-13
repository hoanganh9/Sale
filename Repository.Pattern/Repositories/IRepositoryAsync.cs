using Repository.Pattern.DataContext;
using Repository.Pattern.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Repository.Pattern.Repositories
{
    public interface IRepositoryAsync<TEntity> : IRepository<TEntity> where TEntity : class, IObjectState
    {
        //Task ZBulkInsertAsync(IEnumerable<TEntity> entities, int? batchSize = null, bool userTemp = false);

        //Task ZBulkDeleteAsync(IEnumerable<TEntity> entities, int? batchSize = null);

        //Task ZBulkUpdateAsync(IEnumerable<TEntity> entities, int? batchSize = null);

        Task<int> ZDeleteFromQueryAsync(Func<IQueryable<TEntity>, IEnumerable<TEntity>> query, int? batchSize = null);

        Task<int> ZUpdateFromQueryAsync(Func<IQueryable<TEntity>, IEnumerable<TEntity>> query, Expression<Func<TEntity, TEntity>> updateExpression, int? batchSize = null);

        Task<TEntity> FindAsync(params object[] keyValues);

        Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues);

        Task<bool> DeleteAsync(params object[] keyValues);

        Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues);

        IRepositoryAsync<T> GetRepositoryAsync<T>() where T : class, IObjectState;

        Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters);

        Task<int> SaveChangesAsync();

        IDataContextAsync GetContextAsync();
    }
}