using DBZion.DAL.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace DBZion.DAL.EF
{
    public class EFDbContext : DbContext
    {
        public DbSet<ArchivedOrder> Archive { get; set; }
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

        private void FixEfProviderServicesProblem()
        {
            // The Entity Framework provider type 'System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer'
            // for the 'System.Data.SqlClient' ADO.NET provider could not be loaded. 
            // Make sure the provider assembly is available to the running application. 
            // See http://go.microsoft.com/fwlink/?LinkId=260882 for more information.
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
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
