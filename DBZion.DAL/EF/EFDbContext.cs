using DBZion.DAL.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace DBZion.DAL.EF
{
    public class EFDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }

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
