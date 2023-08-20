using CQRS.Core.Domain;
using CQRS.Core.Event;
using CQRS.Core.Events;
using CQRS.Core.Exceptions;
using CQRS.Core.Infrastuctur;
using Employee.Cmd.Domain.Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Cmd.Infrastructure.Stores
{
    public class EventStore : IEventStore
    {
        private readonly IEventStoreRepository _eventStoreRepository;
        public EventStore(IEventStoreRepository eventStoreRepository)
        {
            _eventStoreRepository = eventStoreRepository;

        }
        public async Task<List<BaseEvent>> GetEventsAsync(Guid AggregateId)
        {
            var eventStream = await _eventStoreRepository.FindByAggregateIdAsync(AggregateId);
            if (eventStream is null || eventStream.Any())
                throw new AggregatenotFoundExceptions("notFound");
            return eventStream.OrderByDescending(x => x.Version).Select(x => x.EventData).ToList();
        }

        public async Task SaveEventAsync(Guid Id, IEnumerable<BaseEvent> events, int expectedVersion)
        {
            var eventStream = await _eventStoreRepository.FindByAggregateIdAsync(Id);
            //^1 means event stream lenght minus 1 
            if(expectedVersion != -1 && eventStream[^1].Version != -1)
                throw new ConcurencyException(  );
            var version = expectedVersion;
            foreach(var eve in events)
            {
                version++;
                eve.Version= version;
                var eventType = eve.GetType().Name;
                var eventModel = new EventModel
                {
                    Time = DateTime.Now,
                    AggregateIdentifier = Id,
                    AggregateType = nameof(EmployeeAggregate),
                    Version = version,
                    EventData = eve,
                    EventType = eventType

                };
                await _eventStoreRepository.SaveAsync(eventModel);
            }
            

        }
    }
}
