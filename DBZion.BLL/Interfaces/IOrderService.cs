using DBZion.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DBZion.BLL.Interfaces
{
    public interface IOrderService
    {
        void AddOrder(string userSurname, string userFirstName, string userMiddleName, string phoneNumber, string serviceType, int price, DateTime orderDate, string description, string note, bool isReady, bool call);
        List<Order> GetOrders();
        List<Order> GetOrders(Func<Order, bool> predicate);
        List<X> GetFieldValues<X>(Func<Order, X> selector);
        Task<List<X>> GetFieldValuesAsync<X>(Expression<Func<Order, X>> selector);
        Task<List<Order>> GetOrdersAsync();
        Task<List<Order>> GetOrdersAsync(Expression<Func<Order, bool>> predicate);

        void AddUser(string surname, string firstName, string middleName, string phoneNumber);
        User FindUser(int id);
        User FindUser(Func<User, bool> predicate);
        Task<User> FindUserAsync(int id);
        Task<User> FindUserAsync(Expression<Func<User, bool>> predicate);
        List<User> FindUsersBySurname(string surname);
        Task<List<User>> FindUsersBySurnameAsync(string surname);
        List<User> GetAllUsers();
        Task<List<User>> GetAllUsersAsync();
        List<Order> GetUserOrders(User user);
    }
}
