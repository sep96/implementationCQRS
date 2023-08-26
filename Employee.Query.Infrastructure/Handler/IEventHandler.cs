using Employee.Common.Event;
using Employee.Query.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Query.Infrastructure.Handler
{
    public interface IEventHandler
    {
        Task On(EmployeeCreatedEvent @event);
        Task On(AddVacationEvent @event);
        Task On(DayWorkEvent @event);
        Task On(DeleteEmployeeEvent @event);
        Task On(UpdateEmployeeEvent @event);
    }
}
