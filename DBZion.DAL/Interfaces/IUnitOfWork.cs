using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBZion.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IOrderRepository Orders { get; }

        IUserRepository Users { get; }

        void Save();
    }
}
