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
            return db.Orders.Include(p => p.User).Where(p => p.OrderId == id).FirstOrDefault();
        }

        public async Task<Order> FindByIdAsync(int id)
        {
            return await db.Orders.Include(p => p.User).Where(p => p.OrderId == id).FirstOrDefaultAsync();
        }

        public List<Order> GetAll()
        {
            return db.Orders.Include(c => c.User).ToList();
        }

        public List<Order> GetAll(Func<Order, bool> predicate)
        {
            return db.Orders.Include(p => p.User).Where(predicate).ToList();
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await db.Orders.Include(p => p.User).ToListAsync();
        }

        public async Task<List<Order>> GetAllAsync(Expression<Func<Order, bool>> predicate)
        {
            return await db.Orders.Include(p => p.User).ToListAsync();
        }

        public List<X> GetPropValues<X>(Func<Order, X> selector)
        {
            return db.Orders.Select(selector).Distinct().ToList();
        }

        public List<X> GetPropValues<X>(Func<Order, bool> predicate, Func<Order, X> selector)
        {
            return db.Orders.Where(predicate).Select(selector).Distinct().ToList();
        }

        public async Task<List<X>> GetPropValuesAsync<X>(Expression<Func<Order, X>> selector)
        {
            return await db.Orders.Select(selector).Distinct().ToListAsync();
        }

        public async Task<List<X>> GetPropValuesAsync<X>(Expression<Func<Order, bool>> predicate, Expression<Func<Order, X>> selector)
        {
            return await db.Orders.Where(predicate).Select(selector).Distinct().ToListAsync();
        }
    }
}
