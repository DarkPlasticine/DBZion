using System;

namespace DBZion.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IArchiveRepository Archive { get; }

        IOrderRepository Orders { get; }

        IUserRepository Users { get; }

        void Save();
    }
}
