using implementationCQRS.Dtos;
using implementationCQRS.Models;
using implementationCQRS.Queries.Caching;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace implementationCQRS.Queries
{
    public class EmployeeQueries : IRequest<Employee>, ICachableQuery
    {
        public int Id { get; init; }
        public string Key => $"{nameof(EmployeeQueries)}-{Id}";
    
        private readonly IEmployeeQueriesRepository _repository;
        public EmployeeQueries(IEmployeeQueriesRepository repository)
        {
            _repository = repository;
        }
        public EmployeeDTO FindByID(int employeeID)
        {
            var emp = _repository.GetByID(employeeID);
            return new EmployeeDTO
            {
                Id = emp.Id,
                FullName = emp.FirstName + " " + emp.LastName,
                Address = emp.Street + " " + emp.City + " " + emp.PostalCode,
                Age = Convert.ToInt32(Math.Abs(((DateTime.Now - emp.DateOfBirth).TotalDays) / 365)) - 1
            };
        }
    }
}
