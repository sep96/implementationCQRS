using Confluent.Kafka;
using CQRS.Core.Consumer;
using CQRS.Core.Events;
using Employee.Query.Infrastructure.Converter;
using Employee.Query.Infrastructure.Handler;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Employee.Query.Infrastructure.Consumer
{
    public class EventConsumer : IEventConsumer
    {
        private readonly ConsumerConfig _confug;
        private readonly IEventHandler _handler;

        public EventConsumer(IOptions<ConsumerConfig> options , IEventHandler handler)
        {
            _confug = options.Value;
            _handler = handler;
        }

        public void Consumer(string topic)
        {
            // Create new kafka Statment
            using (var consumer = new ConsumerBuilder<string , string>(_confug)
                .SetKeyDeserializer(Deserializers.Utf8).SetValueDeserializer(Deserializers.Utf8)
                .Build()
                )
            {
                consumer.Subscribe(topic);
                while (true)
                {
                    var consumerResult = consumer.Consume();
                    if (consumerResult?.Message == null)
                        continue;
                    var option = new JsonSerializerOptions
                    {
                        Converters = { new EventJsonConverter() }
                    };
                    // base Event is abstract but we useing event json converter to do Polymorrphic jjson serializer 
                    var @event = JsonSerializer.Deserialize<BaseEvent>(consumerResult.Message.Value, option);
                    var hanlderMethod = _handler.GetType().GetMethod("on", new Type[] { @event.GetType() });
                    if (hanlderMethod is null)
                        throw new Exception("Could not find Event Method");
                    hanlderMethod.Invoke(_handler, new object[] { @event });
                    //Tell Kafka we have successfully consumed and handle the event and the commit method that we invoekd 

                    consumer.Commit(consumerResult);

                }

            }
             
        }
    }
}
