using CQRS.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Infrastuctur
{
    public interface IEventStore
    {
        Task SaveEventAsync(Guid Id, IEnumerable<BaseEvent> events, int expectedVersion);
        Task<List<BaseEvent>> GetEventsAsync(Guid AggregateId);
    }
}
