using DBZion.DAL.EF;
using DBZion.DAL.Entities;
using DBZion.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
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

        public void DeleteRange(IEnumerable<User> users)
        {
            db.Users.RemoveRange(users);
        }

        public User Find(int id)
        {
            return db.Users.Include(p => p.Orders).FirstOrDefault(p => p.UserID == id);
        }

        public async Task<User> FindAsync(int id)
        {
            return await db.Users.Include(p => p.Orders).FirstOrDefaultAsync(p => p.UserID == id);
        }

        public List<User> GetAll()
        {
            return db.Users.AsNoTracking().ToList();
        }

        public List<User> GetAll(Func<User, bool> predicate)
        {
            return db.Users.AsNoTracking().Where(predicate).ToList();
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await db.Users.AsNoTracking().ToListAsync();
        }

        public async Task<List<User>> GetAllAsync(Expression<Func<User, bool>> predicate)
        {
            return await db.Users.AsNoTracking().Where(predicate).ToListAsync();
        }

        public User Find(Func<User, bool> predicate)
        {
            return db.Users.Where(predicate).FirstOrDefault();
        }

        public async Task<User> FindAsync(Expression<Func<User, bool>> predicate)
        {
            return await db.Users.Where(predicate).FirstOrDefaultAsync();
        }

        public List<Order> GetUserOrders(int userId)
        {
            return db.Users.Include(p => p.Orders).Where(p => p.UserID == userId).AsNoTracking().FirstOrDefault().Orders.ToList();
        }

        public async Task<List<Order>> GetUserOrdersAsync(int userId)
        {
            var user = await db.Users.Include(p => p.Orders).Where(p => p.UserID == userId).AsNoTracking().FirstOrDefaultAsync();
            return user.Orders.ToList();
        }

        public List<User> GetUsersWithOrders()
        {
            return db.Users.Include(p => p.Orders).ToList();
        }

        public List<X> GetPropValues<X>(Func<User, X> selector)
        {
            return db.Users.Select(selector).Distinct().ToList();
        }

        public async Task<List<X>> GetPropValuesAsync<X>(Expression<Func<User, X>> selector)
        {
            return await db.Users.Select(selector).Distinct().ToListAsync();
        }
    }
}
