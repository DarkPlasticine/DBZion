﻿using DBZion.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DBZion.DAL.Interfaces
{
    public interface IOrderRepository
    {
        void Add(Order order);

        void Update(Order order);

        void Delete(Order order);

        Order FindById(int id);

        Task<Order> FindByIdAsync(int id);

        List<Order> GetAll();

        List<Order> GetAll(Func<Order, bool> predicate);

        Task<List<Order>> GetAllAsync();

        Task<List<Order>> GetAllAsync(Expression<Func<Order, bool>> predicate);
    }
}
