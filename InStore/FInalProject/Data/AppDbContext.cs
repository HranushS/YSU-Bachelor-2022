using Microsoft.EntityFrameworkCore;
using main.Models;
using Microsoft.Data.SqlClient;
using System.Data.Entity.ModelConfiguration.Conventions;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace MyFinalProjectWeb.Data
{

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            

            //One to many relationship
            modelBuilder.Entity<Stock>()
               .HasOne(x => x.products)
               .WithMany(y => y.stocks);

            modelBuilder.Entity<Stock>()
                .HasKey(z => new { z.store_id , z.product_id});

            modelBuilder.Entity<Store>()
                .HasMany(z => z.stocks)
                .WithOne(x => x.stores);

            modelBuilder.Entity<Product>()
                .HasKey(z => new { z.product_id });

            modelBuilder.Entity<Store>()
                .HasKey(z => new { z.store_id });

    

            modelBuilder.Entity<Orders>()
                .HasKey(x => x.order_id);

            modelBuilder.Entity<Orders>()
                .Property(e => e.order_status)
                .HasConversion<string>();
            modelBuilder.Entity<Orders>()
                .Property(e => e.type)
                .HasConversion<string>();

            modelBuilder.Entity<Orders>()
               .HasOne(x => x.customer)
               .WithMany(y => y.orders);

            modelBuilder.Entity<Orders>()
               .HasOne(x => x.store)
               .WithMany(y => y.orders);

            modelBuilder.Entity<Orders>()
               .HasOne(x => x.products)
               .WithMany(y => y.orders);

            modelBuilder.Entity<Staffs>()
              .HasKey(z => new { z.staff_id });
            modelBuilder.Entity<Staffs>()
                .Property(e => e.position)
                .HasConversion<string>();

            modelBuilder.Entity<Staffs>()
                                    .HasOne(c => c.stores)
                                    .WithMany(p => p.staffs)
                                    .HasForeignKey(c => c.store_id);

            modelBuilder.Entity<Users>()
                .HasKey(c => c.user_id);
        }

        public DbSet<main.Models.Stock> Stock { get; set; }
        public DbSet<main.Models.Store> Stores { get; set; }
        public DbSet<main.Models.Product> Product { get; set; }
        public DbSet<main.Models.Customer> Customer { get; set; }
        public DbSet<main.Models.Staffs>  Staffs { get; set; }
        public DbSet<main.Models.Orders> Orders { get; set; }
        public DbSet<main.Models.Users>  Users { get; set; }
        //public DbSet<main.Models.Register> Registers { get; set; }
    }
}