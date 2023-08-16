using CQRS.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Common.Event
{
    public class UpdateEmployeeEvent : BaseEvent
    {
        public UpdateEmployeeEvent() : base(nameof(UpdateEmployeeEvent))
        {

        }
        public string Department { get; set; }
    }
}
