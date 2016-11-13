using Repository.Pattern.Infrastructure;
using System;

namespace Repository.Pattern.DataContext
{
    public interface IDataContext : IDisposable
    {
        int SaveChanges();

        void SyncObjectState<TEntity>(TEntity entity) where TEntity : class, IObjectState;

        void SyncObjectsStatePostCommit();

        int ExecuteSqlCommand(string sql, params object[] parameters);
    }
}