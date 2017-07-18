using DBZion.DAL.EF;
using DBZion.DAL.Interfaces;
using System;
using System.Threading.Tasks;

namespace DBZion.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private EFDbContext db;
        private OrderRepository orderRepository;
        private UserRepository userRepository;

        public EFUnitOfWork(string connectionString)
        {
            db = new EFDbContext(connectionString);
        }

        public IOrderRepository Orders
        {
            get
            {
                if (orderRepository == null)
                    orderRepository = new OrderRepository(db);
                return orderRepository;
            }
        }

        public IUserRepository Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(db);
                return userRepository;
            }
        }

        public System.Data.Entity.Database Database
        {
            get
            {
                return db.Database;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
