using DBZion.BLL.Interfaces;
using DBZion.DAL.Entities;
using DBZion.DAL.Interfaces;
using DBZion.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public void AddOrder(string serviceType, int price, User user, DateTime orderDate, string description, string note)
        {
            Order order = new Order(serviceType, price, user, orderDate, description, note);
            db.Orders.Add(order);
            db.Save();
        }

        #region Работа с пользователями

        /// <summary>
        /// Добавляет пользователя в базу данных.
        /// </summary>
        /// <param name="surname"></param>
        /// <param name="firstName"></param>
        /// <param name="middleName"></param>
        /// <param name="phoneNumber"></param>
        public void AddUser(string surname, string firstName, string middleName, string phoneNumber)
        {
            User user = new User(surname, firstName, middleName, phoneNumber);
            db.Users.Add(user);
            db.Save();
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

        #endregion

    }
}
