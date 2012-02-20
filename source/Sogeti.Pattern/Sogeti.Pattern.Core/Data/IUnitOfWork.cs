using System;

namespace Sogeti.Pattern.Data
{
    /// <summary>
    /// Unit of work specification
    /// </summary>
    /// <remarks>All implementors should Rollback transactions when disposing (if SaveChanges have not been invoked)</remarks>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Save changes in the data source.
        /// </summary>
        void SaveChanges();
    }
}
