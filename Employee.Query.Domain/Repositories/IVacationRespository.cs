using Employee.Query.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Query.Domain.Repositories
{
    public interface IVacationRespository
    {
        Task CreateAsync(VacationEntity entity);
        Task UpdateAsync(VacationEntity entity);
        Task<VacationEntity> GetByIdAsync(Guid vacationId);
        Task<List<VacationEntity>> GetListAsync();
    }
}
