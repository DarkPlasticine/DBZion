using DBZion.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBZion.DAL.Interfaces
{
    public interface IUserRepository
    {
        void Add(User user);

        void Update(User user);

        void Delete(User user);

        User FindById(int id);

        Task<User> FindByIdAsync(int id);

        List<Order> GetUserOrders(User user);
    }
}
