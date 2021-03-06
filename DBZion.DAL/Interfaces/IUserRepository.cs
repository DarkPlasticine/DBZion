﻿using DBZion.DAL.Entities;
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
        void DeleteRange(IEnumerable<User> users);

        User Find(int id);
        Task<User> FindAsync(int id);
        User Find(Func<User, bool> predicate);
        Task<User> FindAsync(Expression<Func<User, bool>> predicate);

        List<User> GetAll();
        List<User> GetAll(Func<User, bool> predicate);
        Task<List<User>> GetAllAsync();
        Task<List<User>> GetAllAsync(Expression<Func<User, bool>> predicate);

        List<Order> GetUserOrders(int userId);
        Task<List<Order>> GetUserOrdersAsync(int userId);

        List<User> GetUsersWithOrders();

        List<X> GetPropValues<X>(Func<User, X> selector);
        Task<List<X>> GetPropValuesAsync<X>(Expression<Func<User, X>> selector);
    }
}
