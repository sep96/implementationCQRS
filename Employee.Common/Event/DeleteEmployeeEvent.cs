using CQRS.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Common.Event
{
    public class DeleteEmployeeEvent  : BaseEvent
    {
        public DeleteEmployeeEvent() : base(nameof(DeleteEmployeeEvent))
        {

        }
        public Guid Id{ get; set; }
        public string Name { get; set; }
        public DateTime DeletedDateTime { get; set; }
    }
}
