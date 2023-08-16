using CQRS.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Common.Event
{
    public class EmployeeCreatedEvent : BaseEvent
    {
        public EmployeeCreatedEvent() : base(nameof(EmployeeCreatedEvent))
        {

        }
        public string Name { get; set; }
        public string Department { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
