using Repository.Pattern.Infrastructure;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Service.Pattern
{
    public abstract class Service<TEntity> : IService<TEntity> where TEntity : class, IObjectState
    {
        #region Private Fields

        protected readonly IRepositoryAsync<TEntity> _repository;
        protected readonly IUnitOfWorkAsync _unitOfWork;

        #endregion Private Fields

        #region Constructor

        protected Service(IUnitOfWorkAsync unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.RepositoryAsync<TEntity>();
        }

        #endregion Constructor

        public virtual TEntity Find(params object[] keyValues)
        {
            return _repository.Find(keyValues);
        }

        public virtual IQueryable<TEntity> SelectQuery(string query, params object[] parameters)
        {
            return _repository.SelectQuery(query, parameters).AsQueryable();
        }

        public virtual void Insert(TEntity entity)
        {
            _repository.Insert(entity);
        }

        public virtual void InsertRange(IEnumerable<TEntity> entities, int commitCount = 0)
        {
            _repository.InsertRange(entities, commitCount);
        }

        public void BulkInsert(IEnumerable<TEntity> entities, int? batchSize = null)
        {
            _repository.BulkInsert(entities, batchSize);
        }

        public virtual void ZBulkInsert(IEnumerable<TEntity> entities, int? batchSize = null)
        {
            _repository.ZBulkInsert(entities, batchSize);
        }

        public virtual void ZBulkDelete(IEnumerable<TEntity> entities, int? batchSize = null)
        {
            _repository.ZBulkDelete(entities, batchSize);
        }

        public virtual void ZBulkUpdate(IEnumerable<TEntity> entities, int? batchSize = null)
        {
            _repository.ZBulkUpdate(entities, batchSize);
        }

        public virtual void ZDeleteFromQuery(Func<IQueryable<TEntity>, IEnumerable<TEntity>> query, int? batchSize = null)
        {
            _repository.ZDeleteFromQuery(query, batchSize);
        }

        public virtual void ZUpdateFromQuery(Func<IQueryable<TEntity>, IEnumerable<TEntity>> query, Expression<Func<TEntity, TEntity>> updateExpression, int? batchSize = null)
        {
            _repository.ZUpdateFromQuery(query, updateExpression, batchSize);
        }

        //public virtual async Task ZBulkInsertAsync(IEnumerable<TEntity> entities, int? batchSize = null)
        //{
        //    await _repository.ZBulkInsertAsync(entities, batchSize);
        //}

        //public virtual async Task ZBulkDeleteAsync(IEnumerable<TEntity> entities, int? batchSize = null)
        //{
        //    await _repository.ZBulkDeleteAsync(entities, batchSize);
        //}

        //public virtual async Task ZBulkUpdateAsync(IEnumerable<TEntity> entities, int? batchSize = null)
        //{
        //    await _repository.ZBulkUpdateAsync(entities, batchSize);
        //}
        public virtual async Task ZDeleteFromQueryAsync(Func<IQueryable<TEntity>, IEnumerable<TEntity>> query, int? batchSize = null)
        {
            await _repository.ZDeleteFromQueryAsync(query, batchSize);
        }

        public virtual async Task ZUpdateFromQueryAsync(Func<IQueryable<TEntity>, IEnumerable<TEntity>> query, Expression<Func<TEntity, TEntity>> updateExpression, int? batchSize = null)
        {
            await _repository.ZUpdateFromQueryAsync(query, updateExpression, batchSize);
        }

        public virtual void InsertOrUpdateGraph(TEntity entity)
        {
            _repository.InsertOrUpdateGraph(entity);
        }

        public virtual void InsertGraphRange(IEnumerable<TEntity> entities, int commitCount = 0)
        {
            _repository.InsertGraphRange(entities, commitCount);
        }

        public virtual void Update(TEntity entity)
        {
            _repository.Update(entity);
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities, int commitCount = 0)
        {
            _repository.UpdateRange(entities, commitCount);
        }

        public virtual void Delete(object id)
        {
            _repository.Delete(id);
        }

        public virtual void Delete(TEntity entity)
        {
            _repository.Delete(entity);
        }

        public IQueryFluent<TEntity> Query()
        {
            return _repository.Query();
        }

        public virtual IQueryFluent<TEntity> Query(IQueryObject<TEntity> queryObject)
        {
            return _repository.Query(queryObject);
        }

        public virtual IQueryFluent<TEntity> Query(Expression<Func<TEntity, bool>> query)
        {
            return _repository.Query(query);
        }

        public virtual async Task<TEntity> FindAsync(params object[] keyValues)
        {
            return await _repository.FindAsync(keyValues);
        }

        public virtual async Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return await _repository.FindAsync(cancellationToken, keyValues);
        }

        public virtual async Task<bool> DeleteAsync(params object[] keyValues)
        {
            return await DeleteAsync(CancellationToken.None, keyValues);
        }

        //IF 04/08/2014 - Before: return await DeleteAsync(cancellationToken, keyValues);
        public virtual async Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues) { return await _repository.DeleteAsync(cancellationToken, keyValues); }

        public IQueryable<TEntity> Queryable()
        {
            return _repository.Queryable();
        }

        public int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            return _repository.ExecuteSqlCommand(sql, parameters);
        }

        public Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters)
        {
            return _repository.ExecuteSqlCommandAsync(sql, parameters);
        }
    }
}