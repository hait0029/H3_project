using Microsoft.EntityFrameworkCore;

namespace H3_project.Database
{
    public class DatabaseContext :DbContext
    {
        
        public DbSet<Login> Login { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductOrderList> ProductOrderList { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserType> UserType { get; set; }
        public DbSet<Category> Category { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    // Database connection string kun ændre på server navn
        //    base.OnConfiguring(optionsBuilder);
        //    optionsBuilder.UseSqlServer("Data Source=DESKTOP-1GMQO58\\MSSQLSERVER03;Initial Catalog=HMM-Web-Api;Integrated Security=True; Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        //}
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            // Database.EnsureCreated() // ver 2.1
            //Database.EnsureCreated
            //Database.Migrate();
        }

      
    }
}
