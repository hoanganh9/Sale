using Repository.Pattern.DataContext;
using Repository.Pattern.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace Repository.Pattern.Repositories
{
    public interface IRepository<TEntity> where TEntity : class, IObjectState
    {
        TEntity Find(params object[] keyValues);

        IQueryable<TEntity> SelectQuery(string query, params object[] parameters);

        void Insert(TEntity entity);

        void InsertRange(IEnumerable<TEntity> entities, int commitCount = 0);

        void BulkInsert(IEnumerable<TEntity> entities, int? batchSize = null);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities">Danh Sach Insert</param>
        /// <param name="batchSize">So luong ban ghi moi lan insert. Note: Nho hon 0 thi insert luon</param>
        /// <param name="userTemp">False khong dung bang tam, true - insert qua bang tam</param>
        void ZBulkInsert(IEnumerable<TEntity> entities, int? batchSize = null, bool userTemp = false);

        void ZBulkInsertDataTable(DataTable table, int? batchSize = null, bool userTemp = false);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities">Danh Sach Delete</param>
        /// <param name="batchSize">So luong ban ghi moi lan insert. Note: Nho hon 0 thi insert luon</param>
        void ZBulkDelete(IEnumerable<TEntity> entities, int? batchSize = null);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities">Danh Sach update</param>
        /// <param name="batchSize">So luong ban ghi moi lan insert. Note: Nho hon 0 thi insert luon</param>
        void ZBulkUpdate(IEnumerable<TEntity> entities, int? batchSize = null);

        int ZDeleteFromQuery(Func<IQueryable<TEntity>, IEnumerable<TEntity>> query, int? batchSize = null);

        int ZUpdateFromQuery(Func<IQueryable<TEntity>, IEnumerable<TEntity>> query, Expression<Func<TEntity, TEntity>> updateExpression, int? batchSize = null);

        void InsertOrUpdateGraph(TEntity entity);

        void InsertGraphRange(IEnumerable<TEntity> entities, int commitCount = 0);

        void Update(TEntity entity);

        void UpdateRange(IEnumerable<TEntity> entities, int commitCount = 0);

        void Delete(object id);

        void Delete(TEntity entity);

        void Delete(IEnumerable<TEntity> entities);

        IQueryFluent<TEntity> Query(IQueryObject<TEntity> queryObject);

        IQueryFluent<TEntity> Query(Expression<Func<TEntity, bool>> query);

        IQueryFluent<TEntity> Query();

        IQueryable<TEntity> Queryable();

        IEnumerable<EntityModel<TEntity>> ToModel(IEnumerable<TEntity> lstEntity);

        IQueryable<TEntity> Include(params string[] includes);

        IQueryable<TEntity> Include(List<string> lstInclude);

        IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includes);

        IQueryable<TEntity> Include(List<Expression<Func<TEntity, object>>> lstInclude);

        IRepository<T> GetRepository<T>() where T : class, IObjectState;

        int ExecuteSqlCommand(string sql, params object[] parameters);

        int SaveChanges();

        IDataContext GetContext();
    }
}