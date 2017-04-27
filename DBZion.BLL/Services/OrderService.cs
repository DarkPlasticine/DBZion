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
                             string serviceType, int price, DateTime orderDate, string description, string note, bool isReady, bool call)
        {
            try
            {
                User user = FindUser(p => p.Surname == userSurname && p.FirstName == userFirstName && p.MiddleName == userMiddleName && p.PhoneNumber == userPhoneNumber);
                if (user == null)
                {
                    //AddUser(userSurname, userFirstName, userMiddleName, userPhoneNumber);
                    //user = FindUser(p => p.Surname == userSurname && p.FirstName == userFirstName && p.MiddleName == userMiddleName && p.PhoneNumber == userPhoneNumber);
                    user = new User(userSurname, userFirstName, userMiddleName, userPhoneNumber);
                }
                Order order = new Order(AvailableReceiptId(), serviceType, price, orderDate, description, note, isReady, call, user);
                db.Orders.Add(order);
                db.Save();
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при добавлении заказа \n" + ex.Message);
            }
        }

        /// <summary>
        /// Возвращает уникальные значения для выбранного поля.
        /// Использование: var serviceTypes = GetFieldValues(p => p.ServiceType);
        /// </summary>
        /// <typeparam name="X"></typeparam>
        /// <param name="selector"></param>
        /// <returns></returns>
        public List<X> GetFieldValues<X>(Func<Order, X> selector)
        {
            return db.Orders.GetPropValues(selector);
        }

        /// <summary>
        /// Возвращает уникальные значения для выбранного поля.
        /// Использование: var serviceTypes = await GetFieldValuesAsync(p => p.ServiceType);
        /// </summary>
        /// <typeparam name="X"></typeparam>
        /// <param name="selector"></param>
        /// <returns></returns>
        public async Task<List<X>> GetFieldValuesAsync<X>(Expression<Func<Order, X>> selector)
        {
            return await db.Orders.GetPropValuesAsync(selector);
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
        /// Использование: var orders = GetOrders(p => p.ServiceType == "Ремонт");
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
        /// Использование: var orders = await GetOrdersAsync(p => p.ServiceType == "Ремонт");
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

        /// <summary>
        /// Находит пользователя по ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// Использование: var user = await FindUserAsync(p => p.Surname == "Иванов" && p.MiddleName == "Иванович");
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<User> FindUserAsync(Expression<Func<User, bool>> predicate)
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

        /// <summary>
        /// Возвращает список всех пользователей.
        /// </summary>
        /// <returns></returns>
        public List<User> GetAllUsers()
        {
            return db.Users.GetAll();
        }

        /// <summary>
        /// Возвращает список всех пользователей.
        /// </summary>
        /// <returns></returns>
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await db.Users.GetAllAsync();
        }

        /// <summary>
        /// Возвращает список всех заказов пользователя.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public List<Order> GetUserOrders(User user)
        {
            return db.Users.GetUserOrders(user).ToList();
        }

        #endregion

        #region Вспомогательные методы

        /// <summary>
        /// Получает первый свободный номер для Order.ReceiptId
        /// </summary>
        /// <returns></returns>
        private int AvailableReceiptId()
        {
            //получаем все ReceiptId из БД
            List<int> receiptIDS = db.Orders.GetPropValues(p => p.ReceiptId);

            for (int i = 1; i < int.MaxValue; i++)
            {
                if (!receiptIDS.Contains(i))
                    return i;
            }

            return 0;
        }

        #endregion
    }
}
