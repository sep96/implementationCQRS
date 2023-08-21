using Confluent.Kafka;
using CQRS.Core.Events;
using CQRS.Core.Producer;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Employee.Cmd.Infrastructure.Producer
{
    public class EventProducer : IEventProducer
    {
        private readonly ProducerConfig _config;

        public EventProducer(IOptions<ProducerConfig> conf)
        {
            _config = conf.Value;

        }
        public async  Task ProducerAsynce<T>(string topic, T @event) where T : BaseEvent
        {
            using (var producer = new ProducerBuilder<string, string>(_config)
                .SetKeySerializer(Serializers.Utf8)
                .SetValueSerializer(Serializers.Utf8).
                Build())
            {
                var eventmessage = new Message<string, string>
                {
                    Key = Guid.NewGuid().ToString(),
                    Value = JsonSerializer.Serialize(@event, @event.GetType())
                };
                var deliveryResult = await producer.ProduceAsync(topic, @eventmessage);
                if (deliveryResult.Status == PersistenceStatus.NotPersisted)
                    throw new Exception("Could not Produce kafka");
            }
        }
    }
}
