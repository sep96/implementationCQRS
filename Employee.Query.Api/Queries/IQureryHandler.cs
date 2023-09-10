using Employee.Query.Domain.Entity;

namespace Employee.Query.Api.Queries
{
    public interface IQureryHandler
    {
        Task<List<EmployeeEntity>> hadnleAsync(FindAllEmployeeQuery query);
        Task<List<EmployeeEntity>> hadnleAsync(FindbyIdEmployee query);
    }
}
