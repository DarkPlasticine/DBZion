using DBZion.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DBZion.DAL.Interfaces
{
    public interface IUserRepository
    {
        void Add(User user);

        void Update(User user);

        void Delete(User user);

        List<User> GetAll();

        List<User> GetAll(Func<User, bool> predicate);

        Task<List<User>> GetAllAsync();

        Task<List<User>> GetAllAsync(Expression<Func<User, bool>> predicate);

        User GetUser(Func<User, bool> predicate);

        Task<User> GetUserAsync(Expression<Func<User, bool>> predicate);

        User FindById(int id);

        Task<User> FindByIdAsync(int id);

        List<Order> GetUserOrders(User user);

        List<X> GetPropValues<X>(Func<User, bool> predicate, Func<User, X> selector);

        Task<List<X>> GetPropValuesAsync<X>(Expression<Func<User, bool>> predicate, Expression<Func<User, X>> selector);
    }
}
