using DBZion.BLL.Interfaces;
using DBZion.DAL.Entities;
using DBZion.DAL.Interfaces;
using DBZion.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DBZion.BLL.Services
{
    public class OrderService : IOrderService
    {
        private IUnitOfWork db;

        /// <summary>
        /// Инициализирует сервис для работы с базой данных Zion.
        /// </summary>
        /// <param name="connectionString">Строка подключения к базе данных.</param>
        public OrderService(string connectionString)
        {
            db = new EFUnitOfWork(connectionString);
        }

        #region Работа с заказами

        public void AddOrder(string userSurname, string userFirstName, string userMiddleName, string userPhoneNumber, 
                             string serviceType, int price, DateTime orderDate, string description, string note)
        {
            try
            {
                User user = FindUser(p => p.Surname == userSurname && p.FirstName == userFirstName && p.MiddleName == userMiddleName && p.PhoneNumber == userPhoneNumber);
                if (user != null)
                    AddUser(userSurname, userFirstName, userMiddleName, userPhoneNumber);
                Order order = new Order(serviceType, price, orderDate, description, note, user);
                db.Orders.Add(order);
                db.Save();
            }
            catch(Exception ex)
            {
                throw new Exception("Ошибка при добавлении заказа \n" + ex.Message);
            }
        }

        /// <summary>
        /// Возвращает список заказов.
        /// </summary>
        /// <returns></returns>
        public List<Order> GetOrders()
        {
            return db.Orders.GetAll();
        }

        /// <summary>
        /// Возвращает список заказов по определенному условию.
        /// Использование var orders = GetOrders(p => p.ServiceType == "Ремонт");
        /// </summary>
        /// <param name="predicate">Условие.</param>
        /// <returns></returns>
        public List<Order> GetOrders(Func<Order, bool> predicate)
        {
            return db.Orders.GetAll(predicate);
        }

        /// <summary>
        /// Возвращает список заказов.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Order>> GetOrdersAsync()
        {
            return await db.Orders.GetAllAsync();
        }

        /// <summary>
        /// Возвращает список заказов по определенному условию.
        /// Использование: var orders = GetOrders(p => p.ServiceType == "Ремонт");
        /// </summary>
        /// <param name="predicate">Условие.</param>
        /// <returns></returns>
        public async Task<List<Order>> GetOrdersAsync(Expression<Func<Order, bool>> predicate)
        {
            return await db.Orders.GetAllAsync(predicate);
        }

        #endregion


        #region Работа с пользователями

        /// <summary>
        /// Добавляет пользователя в базу данных.
        /// </summary>
        /// <param name="surname">Фамилия</param>
        /// <param name="firstName">Имя</param>
        /// <param name="middleName">Отчество</param>
        /// <param name="phoneNumber">Номер телефона</param>
        public void AddUser(string surname, string firstName, string middleName, string phoneNumber)
        {
            try
            {
                User user = new User(surname, firstName, middleName, phoneNumber);
                db.Users.Add(user);
                db.Save();
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при добавлении пользователя \n" + ex.Message);
            }
        }

        /// <summary>
        /// Находит пользователя по ID.
        /// </summary>
        /// <param name="id">ID.</param>
        /// <returns></returns>
        public User FindUser(int id)
        {
            return db.Users.FindById(id);
        }

        public async Task<User> FindUserAsync(int id)
        {
            return await db.Users.FindByIdAsync(id);
        }

        /// <summary>
        /// Находит пользователя по определенному условию.
        /// Использование: var user = FindUser(p => p.Surname == "Иванов" && p.MiddleName == "Иванович");
        /// </summary>
        /// <param name="predicate">Условие.</param>
        /// <returns></returns>
        public User FindUser(Func<User, bool> predicate)
        {
            return db.Users.GetUser(predicate);
        }

        /// <summary>
        /// Находит пользователя по определенному условию.
        /// Использование: var user = FindUserAsync(p => p.Surname == "Иванов" && p.MiddleName == "Иванович");
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task< User> FindUserAsync(Expression<Func<User, bool>> predicate)
        {
            return await db.Users.GetUserAsync(predicate);
        }

        /// <summary>
        /// Находит всех пользователей с заданной фамилией.
        /// </summary>
        /// <param name="surname">Фамилия или часть фамилии</param>
        /// <returns></returns>
        public List<User> FindUsersBySurname(string surname)
        {
            return db.Users.GetAll(p => p.Surname.Contains(surname));
        }

        /// <summary>
        /// Находит всех пользователей с заданной фамилией.
        /// </summary>
        /// <param name="surname">Фамилия или часть фамилии</param>
        /// <returns></returns>
        public async Task<List<User>> FindUsersBySurnameAsync(string surname)
        {
            return await db.Users.GetAllAsync(p => p.Surname.Contains(surname));
        }

        public List<User> GetAllUsers()
        {
            return db.Users.GetAll();
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await db.Users.GetAllAsync();
        }

        public List<Order> GetUserOrders(User user)
        {
            return db.Users.GetUserOrders(user);
        }

        #endregion
    }
}
