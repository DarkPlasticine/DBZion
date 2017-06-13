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
        void AddOrder(string userSurname, string userFirstName, string userMiddleName, string userPhoneNumber, int receiptId, string receiptType, string serviceType, int price, DateTime orderDate, string description, string note, bool isActive, bool isReady, bool call, string worker);
        void UpdateOrder(int id, bool isActive);
        void UpdateOrder(int id, string userSurname, string userFirstName, string userMiddleName, string userPhoneNumber, int receiptId, string receiptType, string serviceType, int price, string description, string note, bool isActive, bool isReady, bool call, string worker);

        ObservableCollection<Order> GetOrders();
        ObservableCollection<Order> GetOrders(Func<Order, bool> predicate);
        Task<ObservableCollection<Order>> GetOrdersAsync();
        Task<ObservableCollection<Order>> GetOrdersAsync(Expression<Func<Order, bool>> predicate);

        List<X> GetFieldValues<X>(Func<Order, X> selector);
        List<X> GetFieldValues<X>(Func<Order, bool> predicate, Func<Order, X> selector);
        Task<List<X>> GetFieldValuesAsync<X>(Expression<Func<Order, X>> selector);
        Task<List<X>> GetFieldValuesAsync<X>(Expression<Func<Order, bool>> predicate, Expression<Func<Order, X>> selector);


        void AddUser(string surname, string firstName, string middleName, string phoneNumber);
        void UpdateUser(int id, string surname, string firstName, string middleName, string phoneNumber);

        User FindUser(int id);
        User FindUser(Func<User, bool> predicate);
        Task<User> FindUserAsync(int id);
        Task<User> FindUserAsync(Expression<Func<User, bool>> predicate);

        List<User> GetUsers();
        List<User> GetUsers(Func<User, bool> predicate);
        Task<List<User>> GetUsersAsync();
        Task<List<User>> GetUsersAsync(Expression<Func<User, bool>> predicate);

        List<Order> GetUserOrders(User user);

        int RemoveInactiveUsers();
    }
}
