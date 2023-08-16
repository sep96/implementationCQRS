using CQRS.Core.Commands;
using CQRS.Core.Infrastuctur;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Cmd.Infrastructure.Dispatcher
{
    public class CommandDispatcher : ICommandDispatcher
    {
        // filead holding all of our Commands 
        private readonly Dictionary<Type, Func<BaseCommand, Task>> _handler = new Dictionary<Type, Func<BaseCommand, Task>>();
        public void Register<T>(Func<T, Task> handler) where T : BaseCommand
        {
            if(_handler.ContainsKey(typeof(T)))
            {
                throw new Exception("Multiple commands");
            }
            _handler.Add(typeof(T), x => handler((T)x));
            
        }

        public async Task SendAsync(BaseCommand command)
        {
            if(_handler.TryGetValue(command.GetType() , out Func<BaseCommand , Task> handler))
            {
                await handler(command);
            }
            else
            {
                throw new Exception("Commands not registerd");
            }
        }
    }
}
