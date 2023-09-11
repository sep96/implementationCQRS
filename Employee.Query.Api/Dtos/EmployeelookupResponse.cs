using Employee.Query.Domain.Entity;

namespace Employee.Query.Api.Dtos
{
    public class EmployeelookupResponse
    {
        public List<EmployeeEntity> Employees { get; set; }
    }
}
