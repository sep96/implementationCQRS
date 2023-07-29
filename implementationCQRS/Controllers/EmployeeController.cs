using implementationCQRS.Command;
using implementationCQRS.Dtos;
using implementationCQRS.Models;
using implementationCQRS.Queries;
using implementationCQRS.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace implementationCQRS.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public string Test()
        {
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
            return "OK";
        }
        [HttpPost]
        public async Task<ActionResult> CreateEmployee([FromBody] CreateEmployeeCommand createEmployeeCommand)
        {
            var Validation = new CreateEmployeeCommandValidator();
            var result = await Validation.ValidateAsync(createEmployeeCommand);
            if (!result.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, result.Errors);
            }

            EmployeeDTO customer = await _mediator.Send(createEmployeeCommand);
            return Ok(customer);
        }
    }
}
