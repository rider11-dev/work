using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCardSln.Repository.Db
{
    /// <summary>
    /// 数据连接事务的Session接口
    /// </summary>
    public interface IDbSession : IDisposable
    {
        string ConnKey { get; }
        DatabaseType DbType { get; }
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }
        IDbTransaction Begin(IsolationLevel isolation = IsolationLevel.ReadCommitted);
        void Commit();
        void Rollback();
    }

    public class DbSession : IDbSession
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private readonly DatabaseType _dbType;
        private readonly string _connKey;
        private bool _isDisposed = false;

        public string ConnKey
        {
            get { return _connKey; }
        }

        public DatabaseType DbType
        {
            get { return _dbType; }
        }

        public IDbConnection Connection
        {
            get { return _connection; }
        }

        public IDbTransaction Transaction
        {
            get { return _transaction; }
        }

        public DbSession()
        {
            _connKey = DbUtils.DefaultConnectionKey;
            _dbType = DbUtils.GetDbTypeByConnKey(_connKey);
            _connection = DbUtils.CreateDbConnection(_connKey);
        }

        public IDbTransaction Begin(IsolationLevel isolation = IsolationLevel.ReadCommitted)
        {
            if (_connection == null)
            {
                throw new Exception("开启事务错误：_connection为空");
            }
            try
            {
                _connection.Open();
                _transaction = _connection.BeginTransaction();
                return _transaction;
            }
            catch (Exception ex)
            {
                throw new Exception("开启事务错误：" + ex.Message);
            }
        }

        public void Commit()
        {
            if (_transaction != null)
            {
                _transaction.Commit();
                _transaction.Dispose();
                _transaction = null;
            }
        }

        public void Rollback()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                _transaction.Dispose();
            }
        }

        ~DbSession()
        {
            Dispose(false);////释放非托管资源
        }

        public void Dispose()
        {
            Dispose(true);//释放托管资源
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                using (_connection)
                {
                    if (_connection != null)
                    {
                        if (_connection.State != ConnectionState.Closed)
                        {
                            if (_transaction != null)
                            {
                                _transaction.Rollback();
                                _transaction.Dispose();
                            }
                        }
                        _connection.Close();
                        _connection.Dispose();
                    }
                }
                _isDisposed = true;
            }
        }
    }
}
