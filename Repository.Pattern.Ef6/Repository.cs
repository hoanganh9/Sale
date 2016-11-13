#region

using LinqKit;
using Repository.Pattern.DataContext;
using Repository.Pattern.Ef6;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.Map;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

#endregion

namespace Repository.Pattern.Ef6
{
    public class Repository<TEntity> : IRepositoryAsync<TEntity> where TEntity : class, IObjectState
    {
        #region Private Fields

        private readonly IDataContextAsync _context;
        private readonly DbSet<TEntity> _dbSet;
        private readonly IUnitOfWorkAsync _unitOfWork;

        #endregion Private Fields

        public Repository(IDataContextAsync context, IUnitOfWorkAsync unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;

            // Temporarily for FakeDbContext, Unit Test and Fakes
            var dbContext = context as DbContext;

            if (dbContext != null)
            {
                _dbSet = dbContext.Set<TEntity>();
            }
            else
            {
                var fakeContext = context as FakeDbContext;

                if (fakeContext != null)
                {
                    _dbSet = fakeContext.Set<TEntity>();
                }
            }
        }

        public virtual TEntity Find(params object[] keyValues)
        {
            return _dbSet.Find(keyValues);
        }

        public virtual IQueryable<TEntity> SelectQuery(string query, params object[] parameters)
        {
            return _dbSet.SqlQuery(query, parameters).AsQueryable();
        }

        public virtual void Insert(TEntity entity)
        {
            entity.ObjectState = ObjectState.Added; ;
            _dbSet.Attach(entity);
            _context.SyncObjectState(entity);
        }

        public virtual void InsertRange(IEnumerable<TEntity> entities, int commitCount = 0)
        {
            if (commitCount > 0)
            {
                int count = 0;
                foreach (var entity in entities)
                {
                    count++;
                    Insert(entity);
                    if (count == commitCount)
                    {
                        count = 0;
                        _unitOfWork.SaveChanges();
                    }
                }
            }
            else
            {
                foreach (var entity in entities)
                {
                    Insert(entity);
                }
            }
        }

        public virtual void BulkInsert(IEnumerable<TEntity> entities, int? batchSize = null)
        {
            ZBulkInsert(entities, batchSize);
            //var dbContext = _context as DbContext;
            ////BulkInsertOptions opt = new BulkInsertOptions();
            //////opt.SqlBulkCopyOptions = System.Data.SqlClient.SqlBulkCopyOptions.Default;
            ////opt.EnableStreaming = true;
            ////if (batchSize.HasValue)
            ////    opt.BatchSize = batchSize.Value;
            //dbContext.BulkInsert<TEntity>(entities, batchSize);
        }

        public virtual void ZBulkInsert(IEnumerable<TEntity> entities, int? batchSize = null, bool userTemp = false)
        {
            if (entities == null || entities.Count() == 0)
                return;

            DataTable dt = _unitOfWork.MapListToTable<TEntity>(entities);
            CtinMapEntity map = _unitOfWork.GetMapTable<TEntity>();
            string tableName = dt.TableName;
            string scriptBefore = string.Empty;
            string scriptAfter = string.Empty;
            if (userTemp)
            {
                dt.TableName = "#" + tableName + entities.GetHashCode();
                scriptBefore = map.getScriptTable(dt.TableName);
                scriptAfter = map.getScriptInsert(dt.TableName);
            }
            if (batchSize.GetValueOrDefault() < 0)
            {
                var dbContext = _context as DbContext;
                string strCon = dbContext.Database.Connection.ConnectionString;
                using (SqlConnection sqlCon = new SqlConnection(strCon))
                {
                    sqlCon.Open();
                    using (SqlTransaction sqlTran = sqlCon.BeginTransaction())
                    {
                        if (userTemp)
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                cmd.CommandType = CommandType.Text;
                                cmd.Connection = sqlCon;
                                cmd.Transaction = sqlTran;
                                //Create table temp
                                cmd.CommandText = scriptBefore;
                                cmd.ExecuteNonQuery();
                                //BulkCopy
                                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(sqlCon, SqlBulkCopyOptions.Default, sqlTran))
                                {
                                    sqlBulkCopy.DestinationTableName = tableName;
                                    sqlBulkCopy.WriteToServer(dt);
                                }
                                //Delete and remove table temp
                                cmd.CommandText = scriptAfter;
                                cmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            //BulkCopy
                            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(sqlCon, SqlBulkCopyOptions.Default, sqlTran))
                            {
                                sqlBulkCopy.DestinationTableName = tableName;
                                sqlBulkCopy.WriteToServer(dt);
                            }
                        }
                        sqlTran.Commit();
                    }
                }
            }
            else
            {
                _unitOfWork.AddBulk(dt, scriptBefore, scriptAfter, batchSize);
            }
            /*Old Buld
            var dbContext = _context as DbContext;
            string strCon = dbContext.Database.Connection.ConnectionString;

            DataTable dt = _unitOfWork.MapListToTable<TEntity>(entities);
            CtinMapEntity map = _unitOfWork.GetMapTable<TEntity>();
            string tableName = dt.TableName;
            using (SqlConnection sqlCon = new SqlConnection(strCon))
            {
                sqlCon.Open();
                SqlTransaction sqlTran = sqlCon.BeginTransaction();
                string scriptInsert = string.Empty;
                if (userTemp)
                {
                    tableName = "#" + tableName + entities.GetHashCode();
                    //Create Temp
                    SqlCommand cmd = new SqlCommand(map.getScriptTable(tableName), sqlCon);
                    cmd.Transaction = sqlTran;
                    cmd.ExecuteNonQuery();
                    //script Insert
                    scriptInsert = map.getScriptInsert(tableName);
                }
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(sqlCon, SqlBulkCopyOptions.Default, sqlTran))
                {
                    sqlBulkCopy.DestinationTableName = tableName;
                    if (batchSize.HasValue)
                        sqlBulkCopy.BatchSize = batchSize.Value;
                    sqlBulkCopy.WriteToServer(dt);
                }
                if (userTemp)
                {
                    SqlCommand cmd = new SqlCommand(scriptInsert, sqlCon);
                    cmd.Transaction = sqlTran;
                    cmd.ExecuteNonQuery();
                }
                sqlTran.Commit();
            }
            */

            //ZProject
            //var dbContext = _context as DbContext;
            //if (batchSize.HasValue)
            //    DbContextExtensions.BulkInsert<TEntity>(dbContext, entities, operation => { operation.BatchSize = batchSize.Value; });
            //else
            //    DbContextExtensions.BulkInsert<TEntity>(dbContext, entities);
        }

        // TuDD 8/16
        public virtual void ZBulkInsertDataTable(DataTable dataTable, int? batchSize = null, bool userTemp = false)
        {
            if (dataTable == null || dataTable.Rows.Count == 0)
                return;

            //DataTable dt = _unitOfWork.MapListToTable<TEntity>(entities);
            DataTable dt = dataTable;
            CtinMapEntity map = _unitOfWork.GetMapTable<TEntity>();
            string tableName = dt.TableName;
            string scriptBefore = string.Empty;
            string scriptAfter = string.Empty;
            if (userTemp)
            {
                dt.TableName = "#" + tableName + dataTable.GetHashCode();
                scriptBefore = map.getScriptTable(dt.TableName);
                scriptAfter = map.getScriptInsert(dt.TableName);
            }
            _unitOfWork.AddBulk(dt, scriptBefore, scriptAfter, batchSize);
        }

        public virtual void ZBulkDelete(IEnumerable<TEntity> entities, int? batchSize = null)
        {
            if (entities == null || entities.Count() == 0)
                return;
            DataTable dt = _unitOfWork.MapListToTable<TEntity>(entities);
            CtinMapEntity map = _unitOfWork.GetMapTable<TEntity>();
            string tableName = "#" + dt.TableName + entities.GetHashCode();
            string scriptBefore = string.Empty;
            string scriptAfter = string.Empty;
            dt.TableName = tableName;
            scriptBefore = map.getScriptTable(tableName);
            scriptAfter = map.getScriptDelete(tableName);
            if(batchSize.GetValueOrDefault()<0)
            {
                var dbContext = _context as DbContext;
                string strCon = dbContext.Database.Connection.ConnectionString;
                using (SqlConnection sqlCon = new SqlConnection(strCon))
                {
                    sqlCon.Open();
                    using (SqlTransaction sqlTran = sqlCon.BeginTransaction())
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Connection = sqlCon;
                            cmd.Transaction = sqlTran;
                            //Create table temp
                            cmd.CommandText = scriptBefore;
                            cmd.ExecuteNonQuery();
                            //BulkCopy
                            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(sqlCon, SqlBulkCopyOptions.Default, sqlTran))
                            {
                                sqlBulkCopy.DestinationTableName = tableName;
                                sqlBulkCopy.WriteToServer(dt);
                            }
                            //Delete and remove table temp
                            cmd.CommandText = scriptAfter;
                            cmd.ExecuteNonQuery();
                        }
                        sqlTran.Commit();
                    }
                }
            }
            else
            {
                _unitOfWork.AddBulk(dt, scriptBefore, scriptAfter, batchSize);
            }

            /*Old Bulk
            var dbContext = _context as DbContext;
            string strCon = dbContext.Database.Connection.ConnectionString;

            CtinMapEntity map = _unitOfWork.GetMapTable<TEntity>();
            DataTable dt = _unitOfWork.MapListToTable<TEntity>(entities);
            string tableName = "#" + dt.TableName + entities.GetHashCode(); ;
            using (SqlConnection sqlCon = new SqlConnection(strCon))
            {
                sqlCon.Open();
                SqlTransaction sqlTran = sqlCon.BeginTransaction();
                //Create table temp
                SqlCommand cmd = new SqlCommand(map.getScriptTable(tableName), sqlCon);
                cmd.Transaction = sqlTran;
                cmd.ExecuteNonQuery();
                //BulkCopy
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(sqlCon, SqlBulkCopyOptions.Default, sqlTran))
                {
                    sqlBulkCopy.DestinationTableName = tableName;
                    if (batchSize.HasValue)
                        sqlBulkCopy.BatchSize = batchSize.Value;
                    sqlBulkCopy.WriteToServer(dt);
                }
                //Delete and remove table temp
                cmd = new SqlCommand(map.getScriptDelete(tableName), sqlCon);
                cmd.Transaction = sqlTran;
                cmd.ExecuteNonQuery();
                sqlTran.Commit();
            }
            */
            //var dbContext = _context as DbContext;
            //if (batchSize.HasValue)
            //    DbContextExtensions.BulkDelete<TEntity>(dbContext, entities, operation => { operation.BatchSize = batchSize.Value; });
            //else
            //    DbContextExtensions.BulkDelete<TEntity>(dbContext, entities);
        }

        public virtual void ZBulkUpdate(IEnumerable<TEntity> entities, int? batchSize = null)
        {
            if (entities == null || entities.Count() == 0)
                return;

            DataTable dt = _unitOfWork.MapListToTable<TEntity>(entities);
            CtinMapEntity map = _unitOfWork.GetMapTable<TEntity>();
            string tableName = "#" + dt.TableName + entities.GetHashCode();
            string scriptBefore = string.Empty;
            string scriptAfter = string.Empty;
            dt.TableName = tableName;
            scriptBefore = map.getScriptTable(tableName);
            scriptAfter = map.getScriptUpdate(tableName);
            if (batchSize.GetValueOrDefault() < 0)
            {
                var dbContext = _context as DbContext;
                string strCon = dbContext.Database.Connection.ConnectionString;
                using (SqlConnection sqlCon = new SqlConnection(strCon))
                {
                    sqlCon.Open();
                    using (SqlTransaction sqlTran = sqlCon.BeginTransaction())
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Connection = sqlCon;
                            cmd.Transaction = sqlTran;

                            //Create table temp
                            cmd.CommandText = scriptBefore;
                            cmd.ExecuteNonQuery();
                            //BulkCopy
                            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(sqlCon, SqlBulkCopyOptions.Default, sqlTran))
                            {
                                sqlBulkCopy.DestinationTableName = tableName;
                                sqlBulkCopy.WriteToServer(dt);
                            }
                            //Delete and remove table temp
                            cmd.CommandText = scriptAfter;
                            cmd.ExecuteNonQuery();
                        }
                        sqlTran.Commit();
                    }
                }
            }
            else
            {
                _unitOfWork.AddBulk(dt, scriptBefore, scriptAfter, batchSize);
            }
            /*Old Bulk
            var dbContext = _context as DbContext;
            string strCon = dbContext.Database.Connection.ConnectionString;

            CtinMapEntity map = _unitOfWork.GetMapTable<TEntity>();
            DataTable dt = _unitOfWork.MapListToTable<TEntity>(entities);
            string tableName = "#" + dt.TableName + entities.GetHashCode(); ;
            using (SqlConnection sqlCon = new SqlConnection(strCon))
            {
                sqlCon.Open();
                SqlTransaction sqlTran = sqlCon.BeginTransaction();
                //Create table temp
                SqlCommand cmd = new SqlCommand(map.getScriptTable(tableName), sqlCon);
                cmd.Transaction = sqlTran;
                cmd.ExecuteNonQuery();
                //BulkCopy
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(sqlCon, SqlBulkCopyOptions.Default, sqlTran))
                {
                    sqlBulkCopy.DestinationTableName = tableName;
                    if (batchSize.HasValue)
                        sqlBulkCopy.BatchSize = batchSize.Value;
                    sqlBulkCopy.WriteToServer(dt);
                }
                //Update and remove table temp
                cmd = new SqlCommand(map.getScriptUpdate(tableName), sqlCon);
                cmd.Transaction = sqlTran;
                cmd.ExecuteNonQuery();
                sqlTran.Commit();
            }
            */
            //var dbContext = _context as DbContext;
            //if (batchSize.HasValue)
            //    DbContextExtensions.BulkUpdate<TEntity>(dbContext, entities, operation => { operation.BatchSize = batchSize.Value; });
            //else
            //    DbContextExtensions.BulkUpdate<TEntity>(dbContext, entities);
        }

        public virtual int ZDeleteFromQuery(Func<IQueryable<TEntity>, IEnumerable<TEntity>> query, int? batchSize = null)
        {
            var batch = new BatchDelete();

            if (batchSize.HasValue)
            {
                batch.BatchSize = batchSize.Value;
            }
            //return query.Invoke(_dbSet).AsQueryable().Delete();
            return batch.Execute(query.Invoke(_dbSet).AsQueryable());

            //var dbContext = _context as DbContext;
            //if (dbContext.Database.Connection.State != System.Data.ConnectionState.Open)
            //    dbContext.Database.Connection.Open();
            //var bulk = new BulkOperation<TEntity>(dbContext.Database.Connection);
            //if (batchSize.HasValue)
            //    bulk.BatchSize = batchSize.Value;
            //bulk.DeleteFromQuery(query);
        }

        public virtual int ZUpdateFromQuery(Func<IQueryable<TEntity>, IEnumerable<TEntity>> query, Expression<Func<TEntity, TEntity>> updateExpression, int? batchSize = null)
        {
            var batch = new BatchUpdate();

            if (batchSize.HasValue)
            {
                //batch.BatchSize = batchSize.Value;
            }
            //return query.Invoke(_dbSet).AsQueryable().Update(updateExpression);
            return batch.Execute(query.Invoke(_dbSet).AsQueryable(), updateExpression);

            //var dbContext = _context as DbContext;
            //if (dbContext.Database.Connection.State != System.Data.ConnectionState.Open)
            //    dbContext.Database.Connection.Open();
            //var bulk = new BulkOperation<TEntity>(dbContext.Database.Connection);
            //if (batchSize.HasValue)
            //    bulk.BatchSize = batchSize.Value;
            //bulk.UpdateFromQuery(query, updateExpression);
        }

        public virtual void InsertGraphRange(IEnumerable<TEntity> entities, int commitCount = 0)
        {
            _dbSet.AddRange(entities);
        }

        public virtual void Update(TEntity entity)
        {
            entity.ObjectState = ObjectState.Modified;
            _dbSet.Attach(entity);
            _context.SyncObjectState(entity);
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities, int commitCount = 0)
        {
            if (commitCount > 0)
            {
                int count = 0;
                foreach (TEntity entity in entities)
                {
                    count++;
                    this.Update(entity);
                    if (count == commitCount)
                    {
                        count = 0;
                        _unitOfWork.SaveChanges();
                    }
                }
            }
            else
            {
                foreach (TEntity entity in entities)
                {
                    this.Update(entity);
                }
            }
        }

        public virtual void Delete(object id)
        {
            var entity = _dbSet.Find(id);
            Delete(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            entity.ObjectState = ObjectState.Deleted;
            _dbSet.Attach(entity);
            _context.SyncObjectState(entity);
        }

        public virtual void Delete(IEnumerable<TEntity> entities)
        {
            foreach (TEntity entity in entities)
                this.Delete(entity);
        }

        public IQueryFluent<TEntity> Query()
        {
            return new QueryFluent<TEntity>(this);
        }

        public virtual IQueryFluent<TEntity> Query(IQueryObject<TEntity> queryObject)
        {
            return new QueryFluent<TEntity>(this, queryObject);
        }

        public virtual IQueryFluent<TEntity> Query(Expression<Func<TEntity, bool>> query)
        {
            return new QueryFluent<TEntity>(this, query);
        }

        public IQueryable<TEntity> Queryable()
        {
            return _dbSet;
        }

        public IEnumerable<EntityModel<TEntity>> ToModel(IEnumerable<TEntity> lstEntity)
        {
            return lstEntity.Select(a => new EntityModel<TEntity>(a));
        }

        public virtual IQueryable<TEntity> Include(params string[] includes)
        {
            IQueryable<TEntity> query = _dbSet;

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            return query;
        }

        public virtual IQueryable<TEntity> Include(List<string> lstInclude)
        {
            IQueryable<TEntity> query = _dbSet;

            if (lstInclude != null)
            {
                query = lstInclude.Aggregate(query, (current, include) => current.Include(include));
            }
            return query;
        }

        public virtual IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet;

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            return query;
        }

        public virtual IQueryable<TEntity> Include(List<Expression<Func<TEntity, object>>> lstInclude)
        {
            IQueryable<TEntity> query = _dbSet;

            if (lstInclude != null)
            {
                query = lstInclude.Aggregate(query, (current, include) => current.Include(include));
            }
            return query;
        }

        public IRepository<T> GetRepository<T>() where T : class, IObjectState
        {
            return _unitOfWork.Repository<T>();
        }

        public IRepositoryAsync<T> GetRepositoryAsync<T>() where T : class, IObjectState
        {
            return _unitOfWork.RepositoryAsync<T>();
        }

        //public virtual async Task ZBulkInsertAsync(IEnumerable<TEntity> entities, int? batchSize = null, bool userTemp = false)
        //{
        //    if (entities == null || entities.Count() == 0)
        //        return;
        //    var dbContext = _context as DbContext;
        //    string strCon = dbContext.Database.Connection.ConnectionString;

        //    DataTable dt = _unitOfWork.MapListToTable<TEntity>(entities);
        //    CtinMapEntity map = _unitOfWork.GetMapTable<TEntity>();
        //    string tableName = dt.TableName;
        //    using (SqlConnection sqlCon = new SqlConnection(strCon))
        //    {
        //        await sqlCon.OpenAsync();
        //        SqlTransaction sqlTran = sqlCon.BeginTransaction();
        //        string scriptInsert = string.Empty;
        //        if (userTemp)
        //        {
        //            tableName = "#" + tableName + entities.GetHashCode();
        //            //Create Temp
        //            SqlCommand cmd = new SqlCommand(map.getScriptTable(tableName), sqlCon);
        //            cmd.Transaction = sqlTran;
        //            cmd.ExecuteNonQuery();
        //            //script Insert
        //            scriptInsert = map.getScriptInsert(tableName);
        //        }
        //        using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(sqlCon, SqlBulkCopyOptions.Default, sqlTran))
        //        {
        //            sqlBulkCopy.DestinationTableName = tableName;
        //            if (batchSize.HasValue)
        //                sqlBulkCopy.BatchSize = batchSize.Value;
        //            sqlBulkCopy.WriteToServer(dt);
        //        }
        //        if (userTemp)
        //        {
        //            SqlCommand cmd = new SqlCommand(scriptInsert, sqlCon);
        //            cmd.Transaction = sqlTran;
        //            cmd.ExecuteNonQuery();
        //        }
        //        sqlTran.Commit();
        //    }
        //    //var dbContext = _context as DbContext;
        //    //if (batchSize.HasValue)
        //    //    await DbContextExtensions.BulkInsertAsync<TEntity>(dbContext,entities, operation => { operation.BatchSize = batchSize.Value; });
        //    //else
        //    //    await DbContextExtensions.BulkInsertAsync<TEntity>(dbContext,entities);
        //}

        //public virtual async Task ZBulkDeleteAsync(IEnumerable<TEntity> entities, int? batchSize = null)
        //{
        //    if (entities == null || entities.Count() == 0)
        //        return;
        //    var dbContext = _context as DbContext;
        //    string strCon = dbContext.Database.Connection.ConnectionString;

        //    CtinMapEntity map = _unitOfWork.GetMapTable<TEntity>();
        //    DataTable dt = _unitOfWork.MapListToTable<TEntity>(entities);
        //    string tableName = "#" + dt.TableName + entities.GetHashCode(); ;
        //    using (SqlConnection sqlCon = new SqlConnection(strCon))
        //    {
        //        await sqlCon.OpenAsync();
        //        SqlTransaction sqlTran = sqlCon.BeginTransaction();
        //        //Create table temp
        //        SqlCommand cmd = new SqlCommand(map.getScriptTable(tableName), sqlCon);
        //        cmd.Transaction = sqlTran;
        //        cmd.ExecuteNonQuery();
        //        //BulkCopy
        //        using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(sqlCon, SqlBulkCopyOptions.Default, sqlTran))
        //        {
        //            sqlBulkCopy.DestinationTableName = tableName;
        //            if (batchSize.HasValue)
        //                sqlBulkCopy.BatchSize = batchSize.Value;
        //            sqlBulkCopy.WriteToServer(dt);
        //        }
        //        //Delete and remove table temp
        //        cmd = new SqlCommand(map.getScriptDelete(tableName), sqlCon);
        //        cmd.Transaction = sqlTran;
        //        cmd.ExecuteNonQuery();
        //        sqlTran.Commit();
        //    }
        //    //var dbContext = _context as DbContext;
        //    //if (batchSize.HasValue)
        //    //    await DbContextExtensions.BulkDeleteAsync<TEntity>(dbContext, entities, operation => { operation.BatchSize = batchSize.Value; });
        //    //else
        //    //    await DbContextExtensions.BulkDeleteAsync<TEntity>(dbContext, entities);
        //}

        //public virtual async Task ZBulkUpdateAsync(IEnumerable<TEntity> entities, int? batchSize = null)
        //{
        //    if (entities == null || entities.Count() == 0)
        //        return;
        //    var dbContext = _context as DbContext;
        //    string strCon = dbContext.Database.Connection.ConnectionString;

        //    CtinMapEntity map = _unitOfWork.GetMapTable<TEntity>();
        //    DataTable dt = _unitOfWork.MapListToTable<TEntity>(entities);
        //    string tableName = "#" + dt.TableName + entities.GetHashCode(); ;
        //    using (SqlConnection sqlCon = new SqlConnection(strCon))
        //    {
        //        await sqlCon.OpenAsync();
        //        SqlTransaction sqlTran = sqlCon.BeginTransaction();
        //        //Create table temp
        //        SqlCommand cmd = new SqlCommand(map.getScriptTable(tableName), sqlCon);
        //        cmd.Transaction = sqlTran;
        //        cmd.ExecuteNonQuery();
        //        //BulkCopy
        //        using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(sqlCon, SqlBulkCopyOptions.Default, sqlTran))
        //        {
        //            sqlBulkCopy.DestinationTableName = tableName;
        //            if (batchSize.HasValue)
        //                sqlBulkCopy.BatchSize = batchSize.Value;
        //            sqlBulkCopy.WriteToServer(dt);
        //        }
        //        //Update and remove table temp
        //        cmd = new SqlCommand(map.getScriptUpdate(tableName), sqlCon);
        //        cmd.Transaction = sqlTran;
        //        cmd.ExecuteNonQuery();
        //        sqlTran.Commit();
        //    }
        //    //var dbContext = _context as DbContext;
        //    //if (batchSize.HasValue)
        //    //    await DbContextExtensions.BulkUpdateAsync<TEntity>(dbContext, entities, operation => { operation.BatchSize = batchSize.Value; });
        //    //else
        //    //    await DbContextExtensions.BulkUpdateAsync<TEntity>(dbContext, entities);
        //}

        public virtual async Task<int> ZDeleteFromQueryAsync(Func<IQueryable<TEntity>, IEnumerable<TEntity>> query, int? batchSize = null)
        {
            var batch = new BatchDelete();

            if (batchSize.HasValue)
            {
                batch.BatchSize = batchSize.Value;
            }
            //await query.Invoke(_dbSet).AsQueryable().DeleteAsync();
            return await Task.Run(() => batch.Execute(query.Invoke(_dbSet).AsQueryable()));

            //var dbContext = _context as DbContext;
            //if (dbContext.Database.Connection.State != System.Data.ConnectionState.Open)
            //    dbContext.Database.Connection.Open();
            //var bulk = new BulkOperation<TEntity>(dbContext.Database.Connection);
            //if (batchSize.HasValue)
            //    bulk.BatchSize = batchSize.Value;
            //await bulk.DeleteFromQueryAsync(query);
        }

        public virtual async Task<int> ZUpdateFromQueryAsync(Func<IQueryable<TEntity>, IEnumerable<TEntity>> query, Expression<Func<TEntity, TEntity>> updateExpression, int? batchSize = null)
        {
            var batch = new BatchUpdate();

            if (batchSize.HasValue)
            {
                //batch.BatchSize = batchSize.Value;
            }
            //return await query.Invoke(_dbSet).AsQueryable().UpdateAsync(updateExpression);
            return await Task.Run(() => batch.Execute(query.Invoke(_dbSet).AsQueryable(), updateExpression));

            //var dbContext = _context as DbContext;
            //if (dbContext.Database.Connection.State != System.Data.ConnectionState.Open)
            //    dbContext.Database.Connection.Open();
            //var bulk = new BulkOperation<TEntity>(dbContext.Database.Connection);
            //if (batchSize.HasValue)
            //    bulk.BatchSize = batchSize.Value;
            //await bulk.UpdateFromQueryAsync(query, updateExpression);
        }

        public virtual async Task<TEntity> FindAsync(params object[] keyValues)
        {
            return await _dbSet.FindAsync(keyValues);
        }

        public virtual async Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return await _dbSet.FindAsync(cancellationToken, keyValues);
        }

        public virtual async Task<bool> DeleteAsync(params object[] keyValues)
        {
            return await DeleteAsync(CancellationToken.None, keyValues);
        }

        public virtual async Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            var entity = await FindAsync(cancellationToken, keyValues);

            if (entity == null)
            {
                return false;
            }

            entity.ObjectState = ObjectState.Deleted;
            _dbSet.Attach(entity);

            return true;
        }

        internal IQueryable<TEntity> Select(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null,
           List<string> lstincludes = null,
            int? page = null,
            int? pageSize = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            if (lstincludes != null)
            {
                query = lstincludes.Aggregate(query, (current, include) => current.Include(include));
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (filter != null)
            {
                query = query.AsExpandable().Where(filter);
            }
            if (page != null && pageSize != null)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }
            return query;
        }

        internal async Task<IEnumerable<TEntity>> SelectAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null,
            List<string> lstincludes = null,
            int? page = null,
            int? pageSize = null)
        {
            return await Select(filter, orderBy, includes, lstincludes, page, pageSize).ToListAsync();
        }

        public virtual void InsertOrUpdateGraph(TEntity entity)
        {
            SyncObjectGraph(entity);
            _entitesChecked = null;
            _dbSet.Attach(entity);
        }

        private HashSet<object> _entitesChecked; // tracking of all process entities in the object graph when calling SyncObjectGraph

        private void SyncObjectGraph(object entity) // scan object graph for all
        {
            if (_entitesChecked == null)
                _entitesChecked = new HashSet<object>();

            if (_entitesChecked.Contains(entity))
                return;

            _entitesChecked.Add(entity);

            var objectState = entity as IObjectState;

            if (objectState != null && objectState.ObjectState == ObjectState.Added)
                _context.SyncObjectState((IObjectState)entity);

            // Set tracking state for child collections
            foreach (var prop in entity.GetType().GetProperties())
            {
                // Apply changes to 1-1 and M-1 properties
                var trackableRef = prop.GetValue(entity, null) as IObjectState;
                if (trackableRef != null)
                {
                    if (trackableRef.ObjectState == ObjectState.Added)
                        _context.SyncObjectState((IObjectState)entity);

                    SyncObjectGraph(prop.GetValue(entity, null));
                }

                // Apply changes to 1-M properties
                var items = prop.GetValue(entity, null) as IEnumerable<IObjectState>;
                if (items == null) continue;

                Debug.WriteLine("Checking collection: " + prop.Name);

                foreach (var item in items)
                    SyncObjectGraph(item);
            }
        }

        public int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            return _context.ExecuteSqlCommand(sql, parameters);
        }

        public Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters)
        {
            return _context.ExecuteSqlCommandAsync(sql, parameters);
        }

        public IDataContext GetContext()
        {
            return _context;
        }

        public IDataContextAsync GetContextAsync()
        {
            return _context;
        }

        public Task<int> SaveChangesAsync()
        {
            return _unitOfWork.SaveChangesAsync();
        }

        public int SaveChanges()
        {
            return _unitOfWork.SaveChanges();
        }
    }
}