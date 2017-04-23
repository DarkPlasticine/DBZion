using DBZion.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBZion.DAL.Entities;
using System.Linq.Expressions;
using System.Data.Entity;
using DBZion.DAL.EF;

namespace DBZion.DAL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private EFDbContext db;

        public OrderRepository(EFDbContext context)
        {
            db = context;
        }

        public void Add(Order order)
        {
            db.Orders.Add(order);
        }

        public void Delete(Order order)
        {
            db.Orders.Remove(order);
        }

        public void Update(Order order)
        {
            db.Entry(order).State = EntityState.Modified;
        }

        public Order FindById(int id)
        {
            return db.Orders.Find(id);
        }

        public async Task<Order> FindByIdAsync(int id)
        {
            return await db.Orders.FindAsync(id);
        }

        public List<Order> GetAll()
        {
            return db.Orders.Include(c => c.User).ToList();
        }

        public List<Order> GetAll(Func<Order, bool> predicate)
        {
            return db.Orders.Include(p => p.User).Where(predicate).ToList();
        }

        public async Task< List<Order>> GetAllAsync()
        {
            return await db.Orders.Include(p => p.User).ToListAsync();
        }

        public async Task<List<Order>> GetAllAsync(Expression<Func<Order, bool>> predicate)
        {
            return await db.Orders.Include(p => p.User).ToListAsync();
        }
    }
}
