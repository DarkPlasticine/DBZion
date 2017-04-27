using DBZion.DAL.EF;
using DBZion.DAL.Entities;
using DBZion.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DBZion.DAL.Repositories
{
    public class ArchiveRepository : IArchiveRepository
    {
        private EFDbContext db;

        public ArchiveRepository(EFDbContext context)
        {
            db = context;
        }

        public void Add(ArchivedOrder order)
        {
            db.Archive.Add(order);
        }

        public List<ArchivedOrder> GetAll()
        {
            return db.Archive.Include(p => p.User).AsNoTracking().ToList();
        }

        public List<ArchivedOrder> GetAll(Func<ArchivedOrder, bool> predicate)
        {
            return db.Archive.Include(p => p.User).AsNoTracking().Where(predicate).ToList();
        }

        public async Task<List<ArchivedOrder>> GetAllAsync()
        {
            return await db.Archive.Include(p => p.User).AsNoTracking().ToListAsync();
        }

        public async Task<List<ArchivedOrder>> GetAllAsync(Expression<Func<ArchivedOrder, bool>> predicate)
        {
            return await db.Archive.Include(p => p.User).AsNoTracking().Where(predicate).ToListAsync();
        }
    }
}
