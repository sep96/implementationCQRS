using CQRS.Core.Domain;
using CQRS.Core.Handler;
using CQRS.Core.Infrastuctur;
using Employee.Cmd.Domain.Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Cmd.Infrastructure.Hanlder
{
    public class EventSourcingHandler : IEventSourcingHadnler<EmployeeAggregate>
    {
        private readonly IEventStore _eventStore;
        public EventSourcingHandler(IEventStore eventStore)
        {
            _eventStore = eventStore;   

        }
        public async Task<EmployeeAggregate> GetByIdAsync(Guid id)
        {
            var aggreaget = new EmployeeAggregate();
            var events = await _eventStore.GetEventsAsync(id);
            if (events is null || events.Any())
                return aggreaget;
            aggreaget.RelayEvent(events);
            aggreaget.Version = events.Select(x => x.Version).Max();
            return aggreaget;

        }

        public async Task SaveAsync(AggregateRoot agg)
        {
            await _eventStore.SaveEventAsync(agg.Id, agg.getUncommitedChanges, agg.Version);
            agg.MarkchangesAsCommit();

            throw new NotImplementedException();
        }
    }
}
