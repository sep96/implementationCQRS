using Employee.Query.Domain.Entity;
using Employee.Query.Domain.Repositories;
using Employee.Query.Infrastructure.DataAccess;
using implementationCQRS.Models;
using Microsoft.EntityFrameworkCore;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Query.Infrastructure.Repositories
{
    public class VacationRespository : IVacationRespository
    {
        private readonly DataBaseContextFactory _contextFactory;

        public VacationRespository(DataBaseContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task CreateAsync(VacationEntity entity)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                context.Vacations.Add(entity);
                await context.SaveChangesAsync();
            }
        }

        public async Task<VacationEntity> GetByIdAsync(Guid vacationId)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                var result = await context.Vacations.FirstOrDefaultAsync(x=>x.VacationId.Equals(vacationId));
                if (result is null)
                    throw new Exception("Not Found ");
                return result;
            }
        }

        public async Task<List<VacationEntity>> GetListAsync()
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Vacations.ToListAsync();
                
            }
        }

        public async Task UpdateAsync(VacationEntity entity)
        {
            
        }
    }
}
