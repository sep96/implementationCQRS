// Command the Employee Domain to save data
using implementationCQRS.Command;
using implementationCQRS.Models;
using implementationCQRS.Queries;
using implementationCQRS.Repositories;
using System.Reflection;

var employeeCommand = new EmployeeCommands(new EmployeeCommandsRepository());
employeeCommand.SaveEmployeeData(new Employee
{
    Id = 200,
    FirstName = "Jane",
    LastName = "Smith",
    Street = "150 Toronto Street",
    City = "Toronto",
    PostalCode = "j1j1j1",
    DateOfBirth = new DateTime(2002, 2, 2)
});
Console.WriteLine($"Employee data stored");
// Query the Employee Domain to get data
var employeeQuery = new EmployeeQueries(new EmployeeQueriesRepository());
var employee = employeeQuery.FindByID(100);
Console.WriteLine($"Employee ID:{employee.Id}, Name:{employee.FullName}, Address:{employee.Address}, Age:{employee.Age}");
Console.ReadKey();
