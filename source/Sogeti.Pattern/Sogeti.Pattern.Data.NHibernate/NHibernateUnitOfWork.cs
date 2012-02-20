using System;
using NHibernate;

namespace Sogeti.Pattern.Data.NHibernate
{
    /// <summary>
    /// Unit of work implementation for nhibernate
    /// </summary>
    public class NHibernateUnitOfWork : IUnitOfWork
    {
        private readonly ITransaction _transaction;
        private bool _disposed;


        /// <summary>
        /// Initializes a new instance of the <see cref="NHibernateUnitOfWork"/> class.
        /// </summary>
        /// <param name="session">The open nhibernate session.</param>
        public NHibernateUnitOfWork(ISession session)
        {
            _transaction = session.BeginTransaction();
        }

        #region IUnitOfWork Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Save changes in the data source.
        /// </summary>
        public void SaveChanges()
        {
            _transaction.Commit();
        }

        #endregion

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (!_transaction.WasCommitted)
                        _transaction.Rollback();

                    _transaction.Dispose();
                }

                _disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="NHibernateUnitOfWork"/> is reclaimed by garbage collection.
        /// </summary>
        ~NHibernateUnitOfWork()
        {
            Dispose(false);
        }
    }
}