using DBZion.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace DBZion.DAL.EF
{
    public class EFDbContext : DbContext
    {
        static EFDbContext()
        {
            Database.SetInitializer<EFDbContext>(new MyEFDbInitializer());
        }

        public EFDbContext(string connectionString)
            :base(connectionString)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

    class OrderConfiguration : EntityTypeConfiguration<Order>
    {
        public OrderConfiguration()
        {

        }
    }

    class MyEFDbInitializer : DropCreateDatabaseIfModelChanges<EFDbContext>
    {
        protected override void Seed(EFDbContext context)
        {
            
        }
    }
}
