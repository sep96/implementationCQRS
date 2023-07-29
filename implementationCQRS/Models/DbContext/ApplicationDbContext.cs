using Microsoft.EntityFrameworkCore;

namespace implementationCQRS.Models.DbContext
{
    public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {}
        public DbSet<Employee> Employee { get; set; }
    }
}
