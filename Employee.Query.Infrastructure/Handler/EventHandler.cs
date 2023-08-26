using Employee.Common.Event;
using Employee.Query.Domain.Entity;
using Employee.Query.Domain.Repositories;
using implementationCQRS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Query.Infrastructure.Handler
{
    public class EventHandler : IEventHandler
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IVacationRespository _vacationRespository;

        public EventHandler(IEmployeeRepository employeeRepository, IVacationRespository vacationRespository)
        {
            _employeeRepository = employeeRepository;
            _vacationRespository = vacationRespository;
        }

        public async Task On(EmployeeCreatedEvent @event)
        {
            var employee = new EmployeeEntity
            {
                Name= @event.Name,
                Department  =  @event.Department,
                CreatedTime = DateTime.Now,
                Vacations = new List<VacationEntity> () ,
                DayofWorks = 0                 
            };
            await _employeeRepository.CreateAsync(employee);
        }

        public async Task On(AddVacationEvent @event)
        {
            var vacation = new VacationEntity
            {
                StartDate = @event.StartDate,
                TotalDays = @event.TotalDays
            };
            await _vacationRespository.CreateAsync(vacation);
        }

        public async Task On(DayWorkEvent @event)
        {
            var employee = await _employeeRepository.GetByIdAsync(@event.Id);
            if (employee is null) throw new Exception("Not Fround");
            employee.DayofWorks++;
            await _employeeRepository.UpdateAsync(employee);
        }

        public async Task On(DeleteEmployeeEvent @event)
        {
            await _employeeRepository.DeleteAsync(@event.Id);
        }

        public async Task On(UpdateEmployeeEvent @event)
        {
            var employee = await _employeeRepository.GetByIdAsync(@event.Id);
            if (employee is null) throw new Exception("Not Fround");
            employee.Department = @event.Department;
            await _employeeRepository.UpdateAsync(employee);
        }
    }
}
