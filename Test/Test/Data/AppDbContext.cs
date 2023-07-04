using Microsoft.EntityFrameworkCore;
using Test.Data.Entities;

namespace Test.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Invoice> Invoices { get; set; }
        
        //can add here the products table
    }
}
