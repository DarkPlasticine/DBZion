using DBZion.DAL.Entities;
using System;
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
            //инициализация БД с начальными данными
            Database.SetInitializer<EFDbContext>(new MyEFDbInitializer());
        }

        public EFDbContext(string connectionString)
            : base(connectionString)
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
            User user1 = new User("Валеронов", "Валерон", "Валеронович", "89995554433");
            User user2 = new User("Олегов", "Олег", "Олегович", "89266363636");
            User user3 = new User("Капитонов", "Капитон", "Капитонович", "89536954306");

            DateTime datetime1 = new DateTime(2016, 3, 25, 16, 26, 12);
            DateTime datetime2 = new DateTime(2017, 7, 12, 16, 26, 12);
            DateTime datetime3 = new DateTime(2017, 1, 15, 16, 26, 12);

            Order order1 = new Order(1, "Тест", 500, datetime1, "Описание", "Заметка", true, false, false, user1, "Васыль");
            Order order2 = new Order(1, "Ремонт", 2500, datetime2, "Описание", "Заметка", false, false, false, user2, "Вазген");
            Order order3 = new Order(1, "Поебень", 666, datetime3, "Описание", "Заметка", false, false, false, user3, "Короед");

            context.Orders.Add(order1);
            context.Orders.Add(order2);
            context.Orders.Add(order3);

            context.SaveChanges();
        }
    }
}
