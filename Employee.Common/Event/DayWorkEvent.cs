using CQRS.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Common.Event
{
    public class DayWorkEvent : BaseEvent
    {
        public DayWorkEvent() : base(nameof(DayWorkEvent))
        {

        }
    }
}
