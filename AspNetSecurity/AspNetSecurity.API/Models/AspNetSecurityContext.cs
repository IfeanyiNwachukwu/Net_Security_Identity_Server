using Microsoft.EntityFrameworkCore;

namespace AspNetSecurity.API.Models
{
    public class AspNetSecurityContext : DbContext
    {
        public AspNetSecurityContext(DbContextOptions<AspNetSecurityContext> options):base(options)
        {

        }
        public DbSet<Customer> Customers { get; set; }
    }
}
