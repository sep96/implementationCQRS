using Employee.Query.Domain.Entity;
using Employee.Query.Domain.Repositories;

namespace Employee.Query.Api.Queries
{
    public class QueryHaNDLER : IQureryHandler
    {
        private readonly IEmployeeRepository _employee;

        public QueryHaNDLER(IEmployeeRepository employee)
        {
            _employee = employee;
        }

        public async  Task<List<EmployeeEntity>> hadnleAsync(FindAllEmployeeQuery query)
        {
            return await _employee.GetAllAsync();
        }

        public async Task<List<EmployeeEntity>> hadnleAsync(FindbyIdEmployee query)
        {
            var result = await _employee.GetByIdAsync(query.ID);
            return new List<EmployeeEntity> { result };
        }
    }
}
