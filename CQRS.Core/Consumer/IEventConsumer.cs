using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Consumer
{
    public interface IEventConsumer
    {
        void Consumer(string topic);
    }
}
