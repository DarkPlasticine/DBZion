using DBZion.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DBZion.BLL.Interfaces
{
    public interface IOrderService
    {
        void AddOrder(Order order);
        Task AddOrderAsync(Order order);
        void UpdateOrder(int id, bool isActive);
        void UpdateOrder(int id, Order order);
        void UpdateOrder(int id, int userId);
        Task UpdateOrderAsync(int id, bool isActive);
        Task UpdateOrderAsync(int id, Order order);

        Order FindOrder(int id);
        Task<Order> FindOrderAsync(int id);

        ObservableCollection<Order> GetOrders();
        ObservableCollection<Order> GetOrders(Func<Order, bool> predicate);
        Task<ObservableCollection<Order>> GetOrdersAsync();
        Task<ObservableCollection<Order>> GetOrdersAsync(Expression<Func<Order, bool>> predicate);

        List<X> GetFieldValues<X>(Func<Order, X> selector);
        List<X> GetFieldValues<X>(Func<Order, bool> predicate, Func<Order, X> selector);
        Task<List<X>> GetFieldValuesAsync<X>(Expression<Func<Order, X>> selector);
        Task<List<X>> GetFieldValuesAsync<X>(Expression<Func<Order, bool>> predicate, Expression<Func<Order, X>> selector);


        void AddUser(User user);
        Task AddUserAsync(User user);
        void UpdateUser(int id, User user);
        Task UpdateUserAsync(int id, User user);

        User FindUser(int id);
        User FindUser(Func<User, bool> predicate);
        Task<User> FindUserAsync(int id);
        Task<User> FindUserAsync(Expression<Func<User, bool>> predicate);

        List<User> GetUsers();
        List<User> GetUsers(Func<User, bool> predicate);
        Task<List<User>> GetUsersAsync();
        Task<List<User>> GetUsersAsync(Expression<Func<User, bool>> predicate);

        List<Order> GetUserOrders(int userId);
        Task<List<Order>> GetUserOrdersAsync(int userId);

        int RemoveInactiveUsers();
    }
}
