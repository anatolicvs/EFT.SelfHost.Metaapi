using EFT.Core.Helper;
using System;
using System.Data;


namespace EFT.Core.Repositories.Dapper
{
    public abstract class DapperHelper : IDisposable, IUnitofwork
    {
        public IDbConnection Connection { get { return _connection; } }
        public IDbTransaction Transaction { get { return _transaction; } }

        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private bool _isTransactionStarted;
        //private int? _commandTimeout = null;

        protected DapperHelper()
        {
            _connection = CreateConnection();
            _connection.Open();
        }
        protected abstract IDbConnection CreateConnection();
        public void BeginTransaction()
        {
            if (_isTransactionStarted)
                throw new InvalidOperationException("Transaction is already started.");
            try
            {
                _transaction = _connection.BeginTransaction();
            }
            catch
            {

            }
            _isTransactionStarted = true;
        }
        public void Commit()
        {
            if (!_isTransactionStarted)
                throw new InvalidOperationException("No transaction started.");

            _transaction.Commit();
            _transaction = null;
            _isTransactionStarted = false;
        }
        public void Rollback()
        {
            if (!_isTransactionStarted)
                throw new InvalidOperationException("No transaction started.");

            _transaction.Rollback();
            _transaction.Dispose();
            _transaction = null;
            _isTransactionStarted = false;
        }

        public virtual void Dispose()
        {
            if (_isTransactionStarted)
                Rollback();

            _connection.Close();
            _connection.Dispose();
            _connection = null;
        }
    }
}
