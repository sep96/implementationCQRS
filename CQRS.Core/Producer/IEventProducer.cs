using CQRS.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Producer
{
    public interface IEventProducer
    {
        Task ProducerAsynce<T>(string topic , T @event) where T :BaseEvent;
    }
}
