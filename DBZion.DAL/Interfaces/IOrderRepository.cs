using DBZion.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBZion.DAL.Interfaces
{
    public interface IOrderRepository
    {
        void Add(Order order);

        void Update(Order order);

        void Save(Order order);

        Order FindById(int id);
    }
}
