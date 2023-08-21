using CQRS.Core.Domain;
using Employee.Common.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Cmd.Domain.Aggregate
{
    public class EmployeeAggregate : AggregateRoot
    {
        private bool _active;
        private string _name;
        private readonly Dictionary<Guid,  Tuple<int, DateTime>> _vacation = new();
        public bool Active
        {
            get => _active; set => _active = value;
        }
        public EmployeeAggregate() 
        {

        }
        public EmployeeAggregate(Guid id, string name, string Department)
        {
            RaiseEvent(new EmployeeCreatedEvent
            {
                Id = id,
                Department = Department,
                Name = name,
                CreatedDateTime = DateTime.Now
            });
        }
       
        public void Apply(EmployeeCreatedEvent @event)
        {
            _id = @event.Id;
            _active = true;
            _name = @event.Name;
        }
        public void EditEmployee(string department)
        {
            if (!_active)
            {
                throw new Exception("Yo can not Edit");
            }
            if (string.IsNullOrWhiteSpace(department))
            {
                throw new Exception("input is null");
            }
            RaiseEvent(new UpdateEmployeeEvent
            {
                Id = _id,
                Department = department
            });

        }
        public void Apply(UpdateEmployeeEvent @event)
        {
            _id = @event.Id;
        }
        public void DayAtWork()
        {
            if (!_active)
            {
                throw new Exception("Yo can not Add Days");
            }
            RaiseEvent(new DayWorkEvent
            {
                Id = _id
            });
        }
        public void Apply(DayWorkEvent @event)
        {
            _id = @event.Id;
        }
        public void AddVacation(int TotalDays, DateTime StartDate, DateTime CreatedDate)
        {
            if (!_active)
            {
                throw new Exception("Yo can not Add vaccation");
            }
            RaiseEvent(new AddVacationEvent
            {
                Id = _id,
                StartDate = StartDate,
                CreatedDate = CreatedDate,
                TotalDays = TotalDays
            });
        }
        public void Apply(AddVacationEvent @event)
        {
            _id = @event.Id;
            _vacation.Add(@event.Id, new Tuple<int, DateTime> ( @event.TotalDays, @event.StartDate ));
    
        }
        public void DeleteEmployee(DateTime deletedTime , string Name)
        {
            if (!_active)
            {
                throw new Exception("Yo can not remove Employye");
            }
            RaiseEvent(new DeleteEmployeeEvent
            {
                Id = _id,
                DeletedDateTime= deletedTime,
                Name= Name
            });
        }
        public void Apply(DeleteEmployeeEvent @event)
        {
            _id = @event.Id;
        }
    }//AddVacationEvent
   
}
