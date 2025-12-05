using Microsoft.EntityFrameworkCore;
using PaymentsService.Domain.Entities;

namespace PaymentsService.Infraestructure.Data
{
    public class ApiDbContext : DbContext
    {

        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }

        public DbSet<Payment> Payment => Set<Payment>();
        public DbSet<Customer> Customer => Set<Customer>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Mapear sin schema (usa dbo por defecto)
            modelBuilder.Entity<Payment>().ToTable("Payment");
            modelBuilder.Entity<Customer>().ToTable("Customer");
        }
    }
}

