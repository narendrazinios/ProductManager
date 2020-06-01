using Microsoft.EntityFrameworkCore;
using ProductManager.Core.Models.Product;
using System;

namespace ProductManager.Core
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):
            base(options)
        {


        }
        
        public DbSet<ProductMaster> ProductMasters { get; set; }
        public DbSet<Tenant> Tenants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //modelBuilder.Entity<ProductMaster>().HasData(
            //    new ProductMaster() { PID = "101", Name = "Value 101" }

            //    );

            modelBuilder.Entity<ProductMaster>()
              .Property(e => e.Images)
              .HasConversion(
                  v => string.Join(',', v),
                  v => v.Split(',', StringSplitOptions.RemoveEmptyEntries))
              
              ;


        }
    }
}
