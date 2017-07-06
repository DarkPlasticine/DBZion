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

        public User FindById(int id)
        {
            return db.Users.Find(id);
        }

        public async Task<User> FindByIdAsync(int id)
        {
            return await db.Users.FindAsync(id);
        }

        public List<User> GetAll()
        {
            return db.Users.ToList();
        }

        public List<User> GetAll(Func<User, bool> predicate)
        {
            return db.Users.Where(predicate).ToList();
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await db.Users.ToListAsync();
        }

        public async Task<List<User>> GetAllAsync(Expression<Func<User, bool>> predicate)
        {
            return await db.Users.Where(predicate).ToListAsync();
        }

        public List<User> GetAllANT()
        {
            return db.Users.AsNoTracking().ToList();
        }

        public List<User> GetAllANT(Func<User, bool> predicate)
        {
            return db.Users.AsNoTracking().Where(predicate).ToList();
        }

        public async Task<List<User>> GetAllANTAsync()
        {
            return await db.Users.AsNoTracking().ToListAsync();
        }

        public async Task<List<User>> GetAllANTAsync(Expression<Func<User, bool>> predicate)
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

        public List<User> GetUsersWithOrders()
        {
            return db.Users.Include(p => p.Orders).AsNoTracking().ToList();
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
