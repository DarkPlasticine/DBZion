using System;

namespace DBZion.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IOrderRepository Orders { get; }

        IUserRepository Users { get; }

        void Save();
    }
}
