using CQRS.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Common.Event
{
    public class AddVacationEvent: BaseEvent
    {
        public AddVacationEvent(): base(nameof(AddVacationEvent))
        {

        }
        public Guid Id { get; set; }
        public int TotalDays { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
