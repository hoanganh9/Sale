using Repository.Pattern.DataContext;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.Map;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Repository.Pattern.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();

        SqlTransaction GetTransaction();

        IDataContext GetContext();

        void Dispose(bool disposing);

        IRepository<TEntity> Repository<TEntity>() where TEntity : class, IObjectState;

        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified);

        int ExecuteSqlCommand(string sql, params object[] parameters);

        bool Commit();

        void Rollback();

        DataTable GetTable<TEntity>() where TEntity : class, IObjectState;

        CtinMapEntity GetMapTable<TEntity>() where TEntity : class, IObjectState;

        DataTable MapListToTable<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IObjectState;

        /// <summary>
        /// Dua bang bulk vao queue
        /// </summary>
        /// <param name="dtBulk">Ban can bulk du lieu(Bang bulk = dtBulk.TableName)</param>
        /// <param name="scriptBefore">Script chay truoc khi Bulk(Create table temp)</param>
        /// <param name="scriptAfter">Script chay sau khi bulk(Insert, update, delete, merge...)</param>
        /// <param name="batchSize"></param>
        void AddBulk(DataTable dtBulk, string scriptBefore, string scriptAfter, int? batchSize);
    }
}