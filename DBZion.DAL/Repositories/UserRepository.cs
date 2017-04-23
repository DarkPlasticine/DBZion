using DBZion.DAL.EF;
using DBZion.DAL.Entities;
using DBZion.DAL.Interfaces;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBZion.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private EFDbContext db;

        public UserRepository(EFDbContext context)
        {
            db = context;
        }

        public void Add(User user)
        {
            db.Users.Add(user);
        }

        public void Update(User user)
        {
            db.Entry(user).State = EntityState.Modified;
        }

        public void Delete(User user)
        {
            db.Users.Remove(user);
        }

        public User FindById(int id)
        {
            return db.Users.Find(id);
        }

        public async Task<User> FindByIdAsync(int id)
        {
            return await db.Users.FindAsync(id);
        }

        public List<Order> GetUserOrders(User user)
        {
            return db.Users.Include(p => p.Orders).Where(p => p.UserID == user.UserID).FirstOrDefault().Orders;
        }
    }
}
