using DBZion.DAL.EF;
using DBZion.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBZion.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private EFDbContext db;
        private OrderRepository orderRepository;
        private UserRepository userRepository;

        public IOrderRepository Orders
        {
            get
            {
                return orderRepository;
            }
        }

        public IUserRepository Users
        {
            get
            {
                return userRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
