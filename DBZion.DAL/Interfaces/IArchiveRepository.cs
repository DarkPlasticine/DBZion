using DBZion.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DBZion.DAL.Interfaces
{
    public interface IArchiveRepository
    {
        void Add(ArchivedOrder order);

        List<ArchivedOrder> GetAll();

        List<ArchivedOrder> GetAll(Func<ArchivedOrder, bool> predicate);

        Task<List<ArchivedOrder>> GetAllAsync();

        Task<List<ArchivedOrder>> GetAllAsync(Expression<Func<ArchivedOrder, bool>> predicate);
    }
}
