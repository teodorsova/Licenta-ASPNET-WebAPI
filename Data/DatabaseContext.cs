using Microsoft.EntityFrameworkCore;
using auth.Models;


namespace auth.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<SubscriptionModel>()
               .Property(p => p.Id)
               .ValueGeneratedOnAdd();
               
            modelBuilder.Entity<OrderModel>()
               .Property(p => p.Id)
               .ValueGeneratedOnAdd();

            modelBuilder.Entity<FurnitureModel>()
               .Property(p => p.Id)
               .ValueGeneratedOnAdd();
            
            modelBuilder.Entity<FurnitureOrderModel>()
               .Property(p => p.Id)
               .ValueGeneratedOnAdd();
        }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<SubscriptionModel> Subscriptions { get; set; }
        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<FurnitureModel> Furnitures {get; set;}
        public DbSet<FurnitureOrderModel> FurnituresOrders { get; set; }
    }
}