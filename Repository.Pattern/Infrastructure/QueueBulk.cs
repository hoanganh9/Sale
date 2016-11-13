using System;
using System.Data;

namespace Repository.Pattern.Infrastructure
{
    public class QueueBulk : IDisposable
    {
        private DataTable dt;
        private readonly string scriptBefore;
        private readonly string scriptAfter;
        private bool _disposed;
        private int? batchSize;

        /// <summary>
        ///
        /// </summary>
        /// <param name="dt">Ban can bulk du lieu(TableName=>DB TableName)</param>
        /// <param name="scriptBefore">Script chay truoc khi Bulk(Create table temp)</param>
        /// <param name="_scriptAfter">Script chay sau khi bulk(Insert, update, delete, merge...)</param>
        public QueueBulk(DataTable _dt, string _scriptBefore, string _scriptAfter, int? _batchSize)
        {
            dt = _dt;
            scriptBefore = _scriptBefore;
            scriptAfter = _scriptAfter;
            batchSize = _batchSize;
        }

        public DataTable DTBulk
        {
            get
            {
                return dt;
            }
        }

        public string ScriptBefore
        {
            get
            {
                return scriptBefore;
            }
        }

        public string ScriptAfter
        {
            get
            {
                return scriptAfter;
            }
        }

        public int? BatchSize
        {
            get
            {
                return batchSize;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
            }
            _disposed = true;
        }
    }
}