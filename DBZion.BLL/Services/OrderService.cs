using DBZion.BLL.Interfaces;
using DBZion.DAL.Entities;
using DBZion.DAL.Interfaces;
using DBZion.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DBZion.BLL.Services
{
    public class OrderService : IOrderService, IDisposable
    {
        private IUnitOfWork db;

        /// <summary>
        /// Инициализирует сервис для работы с базой данных Zion.
        /// </summary>
        /// <param name="connectionString">Строка подключения к базе данных.</param>
        public OrderService(string connectionString)
        {
            db = new EFUnitOfWork(connectionString);
            if (!db.Database.Exists())
            {
                db.Dispose();
                throw new Exception("Ошибка подключения к базе данных");
            }
        }

        #region Работа с заказами

        // Добавление нового заказа в базу данных.
        public void AddOrder(Order order)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    //определяем, есть ли указанный пользователь в БД
                    User user = FindUser(p => p.Surname == order.User.Surname && p.FirstName == order.User.FirstName && p.MiddleName == order.User.MiddleName && p.PhoneNumber == order.User.PhoneNumber);
                    if (user != null)
                    {
                        order.User = user;
                        order.UserID = user.UserID;
                    }
                    db.Orders.Add(order);
                    db.Save();
                    transaction.Commit();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
                    {
                        Debug.Write("Object: " + validationError.Entry.Entity.ToString());
                        Debug.Write(" ");
                        foreach (DbValidationError err in validationError.ValidationErrors)
                        {
                            Debug.Write(err.ErrorMessage + " ");
                        }
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Ошибка при добавлении заказа \n" + ex.Message);
                }
            }
        }

        // Добавление нового заказа в базу данных.
        public async Task AddOrderAsync(Order order)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    //определяем, есть ли указанный пользователь в БД
                    User user = await FindUserAsync(p => p.Surname == order.User.Surname && p.FirstName == order.User.FirstName && p.MiddleName == order.User.MiddleName && p.PhoneNumber == order.User.PhoneNumber);
                    if (user != null)
                    {
                        order.User = user;
                        order.UserID = user.UserID;
                    }
                    db.Orders.Add(order);
                    await db.SaveAsync();
                    transaction.Commit();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
                    {
                        Debug.Write("Object: " + validationError.Entry.Entity.ToString());
                        Debug.Write(" ");
                        foreach (DbValidationError err in validationError.ValidationErrors)
                        {
                            Debug.Write(err.ErrorMessage + " ");
                        }
                    }
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

        //изменение параметра IsActive заказа
        public async Task UpdateOrderAsync(int id, bool isActive)
        {
            try
            {
                Order order = await db.Orders.FindByIdAsync(id);
                order.IsActive = isActive;
                db.Orders.Update(order);
                await db.SaveAsync();
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
        public void UpdateOrder(int id, Order order)
        {
            try
            {
                Order oldOrder = db.Orders.FindById(id);
                User user = FindUser(p => p.Surname == order.User.Surname && p.FirstName == order.User.FirstName && p.MiddleName == order.User.MiddleName && p.PhoneNumber == order.User.PhoneNumber);
                if (user != null)
                {
                    oldOrder.User = user;
                    oldOrder.UserID = user.UserID;
                }
                else
                {
                    oldOrder.UserID = 0;
                    oldOrder.User = order.User;
                }

                oldOrder.Call = order.Call;
                oldOrder.Description = order.Description;
                oldOrder.IsActive = order.IsActive;
                oldOrder.IsReady = order.IsReady;
                oldOrder.Note = order.Note;
                oldOrder.Price = order.Price;
                oldOrder.ReceiptType = order.ReceiptType;
                oldOrder.ServiceType = order.ServiceType;

                db.Orders.Update(oldOrder);
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
        public async Task UpdateOrderAsync(int id, Order order)
        {
            try
            {
                Order oldOrder = await db.Orders.FindByIdAsync(id);
                User user = await FindUserAsync(p => p.Surname == order.User.Surname && p.FirstName == order.User.FirstName && p.MiddleName == order.User.MiddleName && p.PhoneNumber == order.User.PhoneNumber);
                if (user != null)
                {
                    order.User = user;
                    order.UserID = user.UserID;
                }
                oldOrder = order;
                db.Orders.Update(oldOrder);
                await db.SaveAsync();
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

        public void UpdateOrder(int id, int userId)
        {
            try
            {
                Order order = db.Orders.FindById(id);
                order.UserID = userId;
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
        /// Возвращает заказ по Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Order FindOrder(int id)
        {
            return db.Orders.FindById(id);
        }

        /// <summary>
        /// Возвращает заказ по Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Order> FindOrderAsync(int id)
        {
            return await db.Orders.FindByIdAsync(id);
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

        #endregion

        #region Работа с пользователями

        /// <summary>
        /// Добавляет пользователя в базу данных.
        /// </summary>
        /// <param name="user"></param>
        public void AddUser(User user)
        {
            try
            {
                if (db.Users.Find(p => p.Surname == user.Surname && p.FirstName == user.FirstName && p.MiddleName == user.MiddleName && p.PhoneNumber == user.PhoneNumber) != null)
                    throw new Exception("Пользователь с указанными данными уже существует.");
                db.Users.Add(user);
                db.Save();
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при добавлении пользователя \n" + ex.Message);
            }
        }

        /// <summary>
        /// Добавляет пользователя в базу данных.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task AddUserAsync(User user)
        {
            try
            {
                if (await db.Users.FindAsync(p => p.Surname == user.Surname && p.FirstName == user.FirstName && p.MiddleName == user.MiddleName && p.PhoneNumber == user.PhoneNumber) != null)
                    throw new Exception("Пользователь с указанными данными уже существует.");
                db.Users.Add(user);
                await db.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при добавлении пользователя \n" + ex.Message);
            }
        }

        /// <summary>
        /// Изменяет пользователя в базе данных.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        public void UpdateUser(int id, User user)
        {
            try
            {
                User dbUser = db.Users.Find(id);
                dbUser.Surname = user.Surname;
                dbUser.FirstName = user.FirstName;
                dbUser.MiddleName = user.MiddleName;
                dbUser.PhoneNumber = user.PhoneNumber;

                db.Users.Update(dbUser);
                db.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception("Данные указанного пользователя были изменены ранее");
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при изменении пользователя \n" + ex.Message);
            }
        }

        /// <summary>
        /// Изменяет пользователя в базе данных.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        public async Task UpdateUserAsync(int id, User user)
        {
            try
            {
                User dbUser = await db.Users.FindAsync(id);
                dbUser.Surname = user.Surname;
                dbUser.FirstName = user.FirstName;
                dbUser.MiddleName = user.MiddleName;
                dbUser.PhoneNumber = user.PhoneNumber;

                db.Users.Update(dbUser);
                await db.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception("Данные указанного пользователя были изменены ранее");
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при изменении пользователя \n" + ex.Message);
            }
        }

        /// <summary>
        /// Находит пользователя по ID.
        /// </summary>
        /// <param name="id">ID.</param>
        /// <returns></returns>
        public User FindUser(int id)
        {
            return db.Users.Find(id);
        }

        /// <summary>
        /// Находит пользователя по ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<User> FindUserAsync(int id)
        {
            return await db.Users.FindAsync(id);
        }

        /// <summary>
        /// Находит пользователя по определенному условию.
        /// Использование: var user = FindUser(p => p.Surname == "Иванов" && p.MiddleName == "Иванович");
        /// </summary>
        /// <param name="predicate">Условие.</param>
        /// <returns></returns>
        public User FindUser(Func<User, bool> predicate)
        {
            return db.Users.Find(predicate);
        }

        /// <summary>
        /// Находит пользователя по определенному условию.
        /// Использование: var user = await FindUserAsync(p => p.Surname == "Иванов" && p.MiddleName == "Иванович");
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<User> FindUserAsync(Expression<Func<User, bool>> predicate)
        {
            return await db.Users.FindAsync(predicate);
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
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Order> GetUserOrders(int userId)
        {
            return db.Users.GetUserOrders(userId);
        }

        /// <summary>
        /// Возвращает список всех заказов пользователя.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<Order>> GetUserOrdersAsync(int userId)
        {
            return await db.Users.GetUserOrdersAsync(userId);
        }

        /// <summary>
        /// Удаляет пользователей, у которых нет заказов и возвращает количество удаленных записей.
        /// </summary>
        public int RemoveInactiveUsers()
        {
            List<User> users = db.Users.GetUsersWithOrders().Where(p => p.Orders.Count == 0).ToList();
            if (users.Count != 0)
            {
                foreach (User u in users)
                {
                    db.Users.Delete(u);
                }
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

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
