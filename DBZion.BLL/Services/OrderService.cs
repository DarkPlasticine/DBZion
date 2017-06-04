using DBZion.BLL.Interfaces;
using DBZion.DAL.Entities;
using DBZion.DAL.Interfaces;
using DBZion.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
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

        // Добавление нового заказа в базу данных.
        public void AddOrder(string userSurname, string userFirstName, string userMiddleName, string userPhoneNumber, int receiptId, string receiptType,
                             string serviceType, int price, DateTime orderDate, string description, string note, bool isActive, bool isReady, bool call, string worker)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    //определяем, есть ли указанный пользователь в БД
                    User user = FindUser(p => p.Surname == userSurname && p.FirstName == userFirstName && p.MiddleName == userMiddleName && p.PhoneNumber == userPhoneNumber);
                    if (user == null)
                        user = new User(userSurname, userFirstName, userMiddleName, userPhoneNumber);
                    Order order = new Order(receiptId, receiptType, serviceType, price, orderDate, description, note, isActive, isReady, call, user, worker);
                    db.Orders.Add(order);
                    db.Save();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Ошибка при добавлении заказа \n" + ex.Message);
                }
            }
        }

        //изменение параметра IsActive заказа
        public void UpdateOrder(int id, bool isActive)
        {
            try
            {
                Order order = db.Orders.FindById(id);
                order.IsActive = isActive;
                db.Orders.Update(order);
                db.Save();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception("Данный заказ ранее уже был изменен. \n" + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при обновлении заказа \n" + ex.Message);
            }
        }

        // Обновление заказа в базе данных.
        public void UpdateOrder(int id, string userSurname, string userFirstName, string userMiddleName, string userPhoneNumber, int receiptId, string receiptType,
                                string serviceType, int price, string description, string note, bool isActive, bool isReady, bool call, string worker)
        {
            try
            {
                Order order = db.Orders.FindById(id);
                //определяем, есть ли указанный пользователь в БД
                User user = FindUser(p => p.Surname == userSurname && p.FirstName == userFirstName && p.MiddleName == userMiddleName && p.PhoneNumber == userPhoneNumber);
                if (user == null)
                    user = new User(userSurname, userFirstName, userMiddleName, userPhoneNumber);
                order.ReceiptId = receiptId;
                order.ReceiptType = receiptType;
                order.ServiceType = serviceType;
                order.Price = price;
                order.Description = description;
                order.Note = note;
                order.IsActive = isActive;
                order.IsReady = isReady;
                order.Call = call;
                order.User = user;
                order.Worker = worker;

                db.Orders.Update(order);
                db.Save();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception("Данный заказ ранее уже был изменен. \n" + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при обновлении заказа \n" + ex.Message);
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
        /// Возвращает уникальные значения для выбранного поля, удовлетворяющие определенному условию.
        /// Использование: var serviceTypes = GetFieldValues(c => c.IsActive == true, p => p.ServiceType);
        /// </summary>
        /// <typeparam name="X"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public List<X> GetFieldValues<X>(Func<Order, bool> predicate, Func<Order, X> selector)
        {
            return db.Orders.GetPropValues(predicate, selector);
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
        /// Возвращает уникальные значения для выбранного поля, удовлетворяющие определенному условию.
        /// Использование: var serviceTypes = await GetFieldValuesAsync(c => c.IsActive == true, p => p.ServiceType);
        /// </summary>
        /// <typeparam name="X"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public async Task<List<X>> GetFieldValuesAsync<X>(Expression<Func<Order, bool>> predicate, Expression<Func<Order, X>> selector)
        {
            return await db.Orders.GetPropValuesAsync(predicate, selector);
        }

        /// <summary>
        /// Возвращает список заказов.
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Order> GetOrders()
        {
            var orders = db.Orders.GetAll();
            return new ObservableCollection<Order>(orders);
        }


        /// <summary>
        /// Возвращает список заказов по определенному условию.
        /// Использование: var orders = GetOrders(p => p.ServiceType == "Ремонт");
        /// </summary>
        /// <param name="predicate">Условие.</param>
        /// <returns></returns>
        public ObservableCollection<Order> GetOrders(Func<Order, bool> predicate)
        {
            var orders = db.Orders.GetAll(predicate);
            return new ObservableCollection<Order>(orders);
        }

        /// <summary>
        /// Возвращает список заказов.
        /// </summary>
        /// <returns></returns>
        public async Task<ObservableCollection<Order>> GetOrdersAsync()
        {
            var orders = await db.Orders.GetAllAsync();
            return new ObservableCollection<Order>(orders);
        }

        /// <summary>
        /// Возвращает список заказов по определенному условию.
        /// Использование: var orders = await GetOrdersAsync(p => p.ServiceType == "Ремонт");
        /// </summary>
        /// <param name="predicate">Условие.</param>
        /// <returns></returns>
        public async Task<ObservableCollection<Order>> GetOrdersAsync(Expression<Func<Order, bool>> predicate)
        {
            var orders = await db.Orders.GetAllAsync(predicate);
            return new ObservableCollection<Order>(orders);
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
        /// Возвращает список всех пользователей.
        /// </summary>
        /// <returns></returns>
        public List<User> GetUsers()
        {
            return db.Users.GetAll();
        }

        /// <summary>
        /// Возвращает всех пользователей, удовлетворяющих определенному условию.
        /// Использование: var users = GetUsers(p => p.Surname.Contains("Иванов"));
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public List<User> GetUsers(Func<User, bool> predicate)
        {
            return db.Users.GetAll(predicate);
        }

        /// <summary>
        /// Возвращает список всех пользователей.
        /// </summary>
        /// <returns></returns>
        public async Task<List<User>> GetUsersAsync()
        {
            return await db.Users.GetAllAsync();
        }

        /// <summary>
        /// Возвращает всех пользователей, удовлетворяющих определенному условию.
        /// Использование: var users = await GetUsersAsync(p => p.Surname.Contains("Иванов"));
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<List<User>> GetUsersAsync(Expression<Func<User, bool>> predicate)
        {
            return await db.Users.GetAllAsync(predicate);
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

        /// <summary>
        /// Удаляет пользователей, у которых нет заказов и возвращает количество удаленных записей.
        /// </summary>
        public int RemoveInactiveUsers()
        {
            List<User> users = db.Users.GetUsersWithOrders().Where(p => p.Orders.Count == 0).ToList();
            if (users.Count != 0)
            {
                db.Users.DeleteRange(users);
                db.Save();
                return users.Count;
            }
            else
                return 0;
        }

        #endregion

        #region Вспомогательные методы

        /// <summary>
        /// Получает первый свободный номер для Order.ReceiptId
        /// </summary>
        /// <returns></returns>
        private int AvailableReceiptId()
        {
            //получаем все ReceiptId активных заказов из БД
            List<int> receiptIDS = db.Orders.GetPropValues(c => c.IsActive == true, p => p.ReceiptId);

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
