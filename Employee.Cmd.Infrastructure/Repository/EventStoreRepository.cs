using CQRS.Core.Domain;
using CQRS.Core.Event;
using Employee.Cmd.Infrastructure.Config;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Cmd.Infrastructure.Repository
{
    public class EventStoreRepository : IEventStoreRepository
    {
        private readonly IMongoCollection<EventModel> _eventStoreCollection;
        public EventStoreRepository(IOptions<MongoDbConfig> conf)
        {
            var mongoClient = new MongoClient(conf.Value.ConnectionString);
            var mongoDataBase = mongoClient.GetDatabase(conf.Value.DataBase);
            _eventStoreCollection = mongoDataBase.GetCollection<EventModel>(conf.Value.Collection);
        }

        public async Task<List<EventModel>> FindByAggregateIdAsync(Guid AggregateId)
        {
            // configu await false used to avoid forcing the callbackto be on original contexxt or schaduler 
            //have benefits shuch as imptoving perfomance and avoiding deadlocks
            return await _eventStoreCollection.Find(x => x.AggregateIdentifier == AggregateId).ToListAsync().ConfigureAwait(false);
        }

      

        public async  Task SaveAsync(EventModel @event)
            => await _eventStoreCollection.InsertOneAsync(@event).ConfigureAwait(false);
       
    }
}
