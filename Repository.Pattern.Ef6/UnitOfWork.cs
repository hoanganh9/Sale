#region

using EntityFramework.MappingAPI;
using EntityFramework.MappingAPI.Extensions;
using Microsoft.Practices.ServiceLocation;
using Repository.Pattern.DataContext;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.Map;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace Repository.Pattern.Ef6
{
    public class UnitOfWork : IUnitOfWorkAsync
    {
        #region Private Fields

        private IDataContextAsync _dataContext;
        private bool _disposed;
        private DbContext _objectContext;
        private SqlConnection _sqlCon;
        private SqlTransaction _transaction;
        private Dictionary<string, dynamic> _repositories;
        private static Dictionary<string, CtinMapEntity> _ctinMap;
        private List<QueueBulk> lstQueueBulk;
        private int batchSize = 1000;
        #endregion Private Fields

        #region Constuctor/Dispose

        public UnitOfWork(IDataContextAsync dataContext)
        {
            _dataContext = dataContext;
            _repositories = new Dictionary<string, dynamic>();
            lstQueueBulk = new List<QueueBulk>();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                // free other managed objects that implement
                // IDisposable only

                try
                {
                    if (_sqlCon != null && _sqlCon.State == ConnectionState.Open)
                    {
                        _sqlCon.Close();
                    }
                }
                catch (ObjectDisposedException)
                {
                    // do nothing, the objectContext has already been disposed
                }

                if (_dataContext != null)
                {
                    _dataContext.Dispose();
                    _dataContext = null;
                }

                if (_objectContext != null)
                {
                    _objectContext.Dispose();
                    _objectContext = null;
                }

                if (lstQueueBulk != null)
                {
                    lstQueueBulk.Clear();
                    lstQueueBulk = null;
                }
            }

            // release any unmanaged objects
            // set the object references to null

            _disposed = true;
        }

        #endregion Constuctor/Dispose

        public int SaveChanges()
        {
            int result = _dataContext.SaveChanges();
            result += this.BulkExecute();
            return result;
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class, IObjectState
        {
            if (ServiceLocator.IsLocationProviderSet)
            {
                return ServiceLocator.Current.GetInstance<IRepository<TEntity>>();
            }

            return RepositoryAsync<TEntity>();
        }

        public async Task<int> SaveChangesAsync()
        {
            int result = await _dataContext.SaveChangesAsync();
            result += await Task.Run(() => BulkExecute());
            return result;
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return _dataContext.SaveChangesAsync(cancellationToken);
        }

        public IRepositoryAsync<TEntity> RepositoryAsync<TEntity>() where TEntity : class, IObjectState
        {
            if (ServiceLocator.IsLocationProviderSet)
            {
                return ServiceLocator.Current.GetInstance<IRepositoryAsync<TEntity>>();
            }

            if (_repositories == null)
            {
                _repositories = new Dictionary<string, dynamic>();
            }

            var type = typeof(TEntity).Name;

            if (_repositories.ContainsKey(type))
            {
                return (IRepositoryAsync<TEntity>)_repositories[type];
            }

            var repositoryType = typeof(Repository<>);

            _repositories.Add(type, Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _dataContext, this));

            return _repositories[type];
        }

        #region Map

        public DataTable GetTable<TEntity>() where TEntity : class, IObjectState
        {
            return GetMapTable<TEntity>().MapTable;
        }

        public CtinMapEntity GetMapTable<TEntity>() where TEntity : class, IObjectState
        {
            if (_ctinMap == null)
            {
                _ctinMap = new Dictionary<string, CtinMapEntity>();
            }

            var typeName = typeof(TEntity).Name;

            if (_ctinMap.ContainsKey(typeName))
            {
                return _ctinMap[typeName];
            }
            //Get Map of TEntity
            IEntityMap<TEntity> entityMap = (_dataContext as DbContext).Db<TEntity>();
            //Get properties of TEntity
            IList<PropertyInfo> properties = typeof(TEntity).GetProperties().ToList();
            //Create Table
            DataTable dt = new DataTable(entityMap.TableName);
            //Create lstMap
            List<CtinMapPropertie> lstMap = new List<CtinMapPropertie>();

            int i = 0;
            StringBuilder sCol = new StringBuilder();
            StringBuilder sColSet = new StringBuilder();
            StringBuilder sColCrt = new StringBuilder();
            StringBuilder sColTemp = new StringBuilder();
            StringBuilder sCreate = new StringBuilder();
            StringBuilder sInsert = new StringBuilder();
            StringBuilder sUpdate = new StringBuilder();
            StringBuilder sUpOrInsert = new StringBuilder();
            StringBuilder sDelete = new StringBuilder();
            StringBuilder sWhereKey = new StringBuilder();
            string fkey = "T.{0} = Temp.{0}";
            DataColumn dc;
            foreach (var pro in entityMap.Properties)
            {
                //Bo qua truong tu tang va khoa ngoai
                if (pro.IsNavigationProperty || pro.IsIdentity) continue;
                dc = new DataColumn(pro.ColumnName);
                Type type;
                if (pro.ColumnName != pro.PropertyName)
                {
                    type = entityMap.Properties.Where(a => a.PropertyName == pro.ColumnName).Select(a => a.Type).First();
                }
                else
                {
                    type = pro.Type;
                }
                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    dc.DataType = type.GetGenericArguments()[0];
                    dc.AllowDBNull = true;
                }
                else
                {
                    dc.DataType = type;
                    dc.AllowDBNull = !pro.IsRequired;
                }
                dt.Columns.Add(dc);
                lstMap.Add(new CtinMapPropertie(pro.ColumnName, properties.Where(a => a.Name == pro.ColumnName).First()));
                //Script ColCreate
                if (sColCrt.Length > 0)
                    sColCrt.Append(",");
                sColCrt.Append(pro.ColumnName + " " + GetDBType(pro, dc.DataType));
                //Script Col
                if (sCol.Length > 0)
                    sCol.Append(",");
                sCol.Append(pro.ColumnName);
                //Script ColTemp
                if (sColTemp.Length > 0)
                    sColTemp.Append(",");
                sColTemp.Append("Temp." + pro.ColumnName);

                if (pro.IsPk)//Key
                {
                    if (sWhereKey.Length > 0)
                        sWhereKey.Append(" And ");
                    sWhereKey.AppendFormat(fkey, pro.ColumnName);
                }
                else//Script ColSet
                {
                    if (sColSet.Length > 0)
                        sColSet.Append(",");
                    sColSet.AppendFormat(fkey, pro.ColumnName);
                }
            }
            //Script Create Table Temp
            sCreate.Append("CREATE TABLE {0} (")
                .Append(sColCrt)
                .Append(");");

            //Script Insert
            sInsert.AppendFormat("INSERT INTO {0}.{1} (", entityMap.Schema, entityMap.TableName)
                .Append(sCol)
                .Append(") (SELECT ")
                .Append(sCol)
                .Append(" FROM {0}); DROP TABLE {0};");

            //Script Update
            sUpdate.Append("UPDATE T SET ")
                .Append(sColSet)
                .AppendFormat(" FROM  {0}.{1}", entityMap.Schema, entityMap.TableName)
                .Append(" T INNER JOIN {0} Temp ON ")
                .Append(sWhereKey)
                .Append("; DROP TABLE {0};");

            //Script UpdateInsert
            sUpOrInsert.AppendFormat("MERGE INTO {0}.{1} AS T ", entityMap.Schema, entityMap.TableName)
                .Append("USING {0} as Temp ")
                .Append("ON ")
                .Append(sWhereKey)
                .Append(" WHEN MATCHED THEN ")
                .Append("UPDATE SET ")
                .Append(sColSet)
                .Append(" WHEN NOT MATCHED THEN ")
                .Append("INSERT (")
                .Append(sCol)
                .Append(") VALUES (")
                .Append(sColTemp)
                .Append(");  DROP TABLE {0};");
            //Script Delete
            sDelete.AppendFormat("DELETE T FROM {0}.{1} AS T ", entityMap.Schema, entityMap.TableName)
                .Append("JOIN {0} AS Temp ")
                .Append("ON ")
                .Append(sWhereKey)
                .Append(";  DROP TABLE {0};");
            //sDelete.AppendFormat("MERGE INTO {0}.{1} AS T ", entityMap.Schema, entityMap.TableName)
            //    .Append("USING {0} AS Temp ")
            //    .Append("ON ")
            //    .Append(sWhereKey)
            //    .Append(" WHEN MATCHED THEN ")
            //    .Append("DELETE;  DROP TABLE {0};");
            //Create CtinMap
            CtinMapEntity mapEntity = new CtinMapEntity(dt, lstMap, sCreate.ToString(),
                sInsert.ToString(), sUpdate.ToString(), sUpOrInsert.ToString(), sDelete.ToString());
            _ctinMap.Add(typeName, mapEntity);
            //Remove StringBuilder
            sCol = sColSet = sColCrt = sColTemp = sCreate = sInsert
                = sUpdate = sUpOrInsert = sDelete = sWhereKey = null;
            return mapEntity;
        }

        public DataTable MapListToTable<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IObjectState
        {
            CtinMapEntity mapEntiry = GetMapTable<TEntity>();
            DataTable dt = mapEntiry.MapTable;
            DataRow dr;
            foreach (var item in entities)
            {
                dr = dt.NewRow();
                foreach (var map in mapEntiry.MapPropertie)
                {
                    dr[map.colName] = map.propertie.GetValue(item) ?? DBNull.Value;
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        /// <summary>
        /// Dua bang bulk vao queue
        /// </summary>
        /// <param name="dtBulk">Ban can bulk du lieu(Bang bulk = dtBulk.TableName)</param>
        /// <param name="scriptBefore">Script chay truoc khi Bulk(Create table temp)</param>
        /// <param name="scriptAfter">Script chay sau khi bulk(Insert, update, delete, merge...)</param>
        public void AddBulk(DataTable dtBulk, string scriptBefore, string scriptAfter, int? batchSize)
        {
            lstQueueBulk.Add(new QueueBulk(dtBulk, scriptBefore, scriptAfter, batchSize));
        }

        private int BulkExecute()
        {
            if (lstQueueBulk == null || lstQueueBulk.Count == 0) return 0;
            int result = 0;
            bool hasTran;
            if (_objectContext == null)
            {
                _objectContext = (DbContext)_dataContext;
                _sqlCon = (SqlConnection)_objectContext.Database.Connection;
            }
            if (_sqlCon.State != ConnectionState.Open)
            {
                _sqlCon.Open();
            }
            if (_transaction == null)
            {
                hasTran = false;
                _transaction = _sqlCon.BeginTransaction();
            }
            else
            {
                hasTran = true;
            }
            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(_sqlCon, SqlBulkCopyOptions.Default, _transaction))
            {
                SqlCommand cmd;
                foreach (QueueBulk bulk in lstQueueBulk)
                {
                    //Before Bulk
                    if (!string.IsNullOrEmpty(bulk.ScriptBefore))
                    {
                        cmd = new SqlCommand(bulk.ScriptBefore, _sqlCon);
                        cmd.Transaction = _transaction;
                        cmd.ExecuteNonQuery();
                    }

                    //Bulk
                    sqlBulkCopy.DestinationTableName = bulk.DTBulk.TableName;
                    if (bulk.BatchSize.HasValue)
                        sqlBulkCopy.BatchSize = bulk.BatchSize.Value;
                    else
                        sqlBulkCopy.BatchSize = batchSize;
                    sqlBulkCopy.WriteToServer(bulk.DTBulk);

                    //After Bulk
                    if (!string.IsNullOrEmpty(bulk.ScriptAfter))
                    {
                        cmd = new SqlCommand(bulk.ScriptAfter, _sqlCon);
                        cmd.Transaction = _transaction;
                        cmd.ExecuteNonQuery();
                    }
                    result += bulk.DTBulk.Rows.Count;
                    //Dispose
                    bulk.Dispose();
                }
                lstQueueBulk.Clear();
            }
            if (!hasTran)
            {
                this.Commit();
            }
            return result;
        }

        private Type getType(IPropertyMap[] lstMap, string colName)
        {
            Type type = lstMap.Where(a => a.PropertyName == colName).Select(a => a.Type).First();
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return type.GetGenericArguments()[0];
            }
            return type;
        }

        private string GetDBType(IPropertyMap map, Type type)
        {
            string datatype = string.Empty;
            switch (type.Name)
            {
                case "Boolean":
                    datatype = "[bit]";
                    break;

                case "Char":
                    datatype = "[char]";
                    break;

                case "SByte":
                    datatype = "[tinyint]";
                    break;

                case "Int16":
                    datatype = "[smallint]";
                    break;

                case "Int32":
                    datatype = "[int]";
                    break;

                case "Int64":
                    datatype = "[bigint]";
                    break;

                case "Byte":
                    datatype = "[tinyint]";
                    break;

                case "UInt16":
                    datatype = "[smallint]";
                    break;

                case "UInt32":
                    datatype = "[int] UNSIGNED";
                    break;

                case "UInt64":
                    datatype = "[bigint] UNSIGNED";
                    break;

                case "Single":
                    datatype = "[real]";
                    break;

                case "Double":
                    datatype = "[float]";
                    break;

                case "Decimal":
                    datatype = "[decimal](" + map.Precision + "," + map.Scale + ")";
                    break;

                case "DateTime":
                    datatype = "[datetime]";
                    break;

                case "Guid":
                    datatype = "[uniqueidentifier]";
                    break;

                case "Object":
                    datatype = "[variant]";
                    break;

                case "String":
                    datatype = "[" + (map.Unicode ? "n" : "") + (map.FixedLength ? "char" : "varchar") + ("](" + (map.MaxLength > 8000 ? "MAX" : "" + map.MaxLength) + ")");
                    break;

                default:
                    datatype = "[nvarchar](MAX)";
                    break;
            }
            datatype += map.IsRequired ? " NOT NULL" : " NULL";
            return datatype;
        }

        #endregion

        #region Unit of Work Transactions

        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            if (_transaction != null) return;

            if (_objectContext == null)
            {
                _objectContext = (DbContext)_dataContext;
                _sqlCon = (SqlConnection)_objectContext.Database.Connection;
                if (_sqlCon.State != ConnectionState.Open)
                {
                    _sqlCon.Open();
                }
            }
            _transaction = _sqlCon.BeginTransaction(isolationLevel);
            _objectContext.Database.UseTransaction(_transaction);
        }

        public SqlTransaction GetTransaction()
        {
            return _transaction;
        }

        public bool Commit()
        {
            _transaction.Commit();
            _transaction = null;
            return true;
        }

        public void Rollback()
        {
            _transaction.Rollback();
            _transaction = null;
            lstQueueBulk.Clear();
            _dataContext.SyncObjectsStatePostCommit();
        }

        public int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            return _dataContext.ExecuteSqlCommand(sql, parameters);
        }

        public Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters)
        {
            return _dataContext.ExecuteSqlCommandAsync(sql, parameters);
        }

        public IDataContext GetContext()
        {
            return _dataContext;
        }

        public IDataContextAsync GetContextAsync()
        {
            return _dataContext;
        }

        #endregion
    }
}