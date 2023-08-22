using Employee.Query.Domain.Entity;
using Employee.Query.Domain.Repositories;
using Employee.Query.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Query.Infrastructure.Repositories
{
    internal class EmployeeRepository : IEmployeeRepository
    {
        private readonly DataBaseContextFactory _contextFactory;

        public EmployeeRepository(DataBaseContextFactory dbcontext)
        {
            _contextFactory = dbcontext;
        }

        public async Task CreateAsync(EmployeeEntity employee)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                context.Employees.Add(employee);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid EmployeeId)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                var employee = await GetByIdAsync(EmployeeId);
                if (employee is null)
                    throw new Exception("NotFound");
                context.Employees.Remove(employee);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<EmployeeEntity>> GetAllAsync()
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
               return  await context.Employees.Include(x=>x.Vacations).ToListAsync();
            }
        }

        public async Task<EmployeeEntity> GetByIdAsync(Guid EmployeeId)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Employees.FirstOrDefaultAsync(x=>x.EmmployeeID.Equals(EmployeeId));
            }
        }

        public async Task UpdateAsync(EmployeeEntity employee)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                context.Employees.Update(employee);
                await context.SaveChangesAsync();
            }   
        }
    }
}
