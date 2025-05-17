using Microsoft.EntityFrameworkCore;

namespace CoreCommerce.Models
{
    public class Context: DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=VELI\\SQLEXPRESS; database=corecommerce; integrated security=true; TrustServerCertificate=True");
        }
        
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
