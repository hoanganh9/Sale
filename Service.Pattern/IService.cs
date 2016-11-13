using Repository.Pattern.Infrastructure;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Service.Pattern
{
    public interface IService<TEntity> where TEntity : IObjectState
    {
        TEntity Find(params object[] keyValues);

        IQueryable<TEntity> SelectQuery(string query, params object[] parameters);

        void Insert(TEntity entity);

        void InsertRange(IEnumerable<TEntity> entities, int commitCount = 0);

        void BulkInsert(IEnumerable<TEntity> entities, int? batchSize = null);

        void ZBulkInsert(IEnumerable<TEntity> entities, int? batchSize = null);

        void ZBulkDelete(IEnumerable<TEntity> entities, int? batchSize = null);

        void ZBulkUpdate(IEnumerable<TEntity> entities, int? batchSize = null);

        void ZDeleteFromQuery(Func<IQueryable<TEntity>, IEnumerable<TEntity>> query, int? batchSize = null);

        void ZUpdateFromQuery(Func<IQueryable<TEntity>, IEnumerable<TEntity>> query, Expression<Func<TEntity, TEntity>> updateExpression, int? batchSize = null);

        //Task ZBulkInsertAsync(IEnumerable<TEntity> entities, int? batchSize = null);

        //Task ZBulkDeleteAsync(IEnumerable<TEntity> entities, int? batchSize = null);

        //Task ZBulkUpdateAsync(IEnumerable<TEntity> entities, int? batchSize = null);

        Task ZDeleteFromQueryAsync(Func<IQueryable<TEntity>, IEnumerable<TEntity>> query, int? batchSize = null);

        Task ZUpdateFromQueryAsync(Func<IQueryable<TEntity>, IEnumerable<TEntity>> query, Expression<Func<TEntity, TEntity>> updateExpression, int? batchSize = null);

        void InsertOrUpdateGraph(TEntity entity);

        void InsertGraphRange(IEnumerable<TEntity> entities, int commitCount = 0);

        void Update(TEntity entity);

        void UpdateRange(IEnumerable<TEntity> entities, int commitCount = 0);

        void Delete(object id);

        void Delete(TEntity entity);

        IQueryFluent<TEntity> Query();

        IQueryFluent<TEntity> Query(IQueryObject<TEntity> queryObject);

        IQueryFluent<TEntity> Query(Expression<Func<TEntity, bool>> query);

        Task<TEntity> FindAsync(params object[] keyValues);

        Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues);

        Task<bool> DeleteAsync(params object[] keyValues);

        Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues);

        IQueryable<TEntity> Queryable();

        int ExecuteSqlCommand(string sql, params object[] parameters);

        Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters);
    }
}