using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Query.Infrastructure.DataAccess
{
    public class DataBaseContextFactory
    {
        private readonly Action<DbContextOptionsBuilder> _dbContext;
        public DataBaseContextFactory(Action<DbContextOptionsBuilder> dbContext)
        {
            _dbContext = dbContext;
        }
        public ApplicationDbContext CreateDbContext()
        {
            DbContextOptionsBuilder<ApplicationDbContext> options = new();
            _dbContext(options);
            return new ApplicationDbContext(options.Options);
        }
    }
}
