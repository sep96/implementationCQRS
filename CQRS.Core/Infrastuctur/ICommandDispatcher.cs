using CQRS.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Infrastuctur
{
    public interface ICommandDispatcher
    {
        //register handler method that useess generic with input paramter generic and output Task 
        // the delegate  is for our command pass in a handler METHOD WICH WILL B E ASYNC TASK
        void Register<T>(Func<T, Task> handler) where T : BaseCommand;
        // aloow us send dispatch our command objecct 
        Task SendAsync(BaseCommand command);
    }
}
