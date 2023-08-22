using Employee.Query.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Query.Domain.Repositories
{
    public interface IEmployeeRepository
    {
        Task CreateAsync(EmployeeEntity employee);
        Task UpdateAsync(EmployeeEntity employee);
        Task DeleteAsync(Guid EmployeeId);
        Task<EmployeeEntity> GetByIdAsync(Guid EmployeeId);
        Task<List<EmployeeEntity>> GetAllAsync();

    }
}
