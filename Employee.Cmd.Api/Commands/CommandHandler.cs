using CQRS.Core.Handler;
using Employee.Cmd.Domain.Aggregate;
using System.Diagnostics.Tracing;

namespace Employee.Cmd.Api.Commands
{
    public class CommandHandler : ICommandHandler
    {
        private readonly IEventSourcingHadnler<EmployeeAggregate> _eventSourcing;
        public CommandHandler(IEventSourcingHadnler<EmployeeAggregate> eventSourcing)
        {
            _eventSourcing = eventSourcing;
        }
        public async Task HandleAsync(NewEmployeeCommands command)
        {
            var aggreaget = new EmployeeAggregate(command.Id, command.Name ,command.Department  );
            await _eventSourcing.SaveAsync( aggreaget );
        }

        public async Task HandleAsync(EditDepartmentCommand command)
        {
            var aggregate = await _eventSourcing.GetByIdAsync(command.Id);
            aggregate.EditEmployee(command.Deprtment);
            await _eventSourcing.SaveAsync(aggregate);
        }

        public async Task HandleAsync(DeleteEmployeeCommand command)
        {
            var aggregate = await _eventSourcing.GetByIdAsync(command.Id);
            aggregate.DayAtWork();
            await _eventSourcing.SaveAsync(aggregate);
        }

        public async Task HandleAsync(AddVacationCommand command)
        {
            var aggregate = await _eventSourcing.GetByIdAsync(command.Id);
            aggregate.AddVacation(command.TotalDays, command.StartDate, DateTime.Now);
            await _eventSourcing.SaveAsync(aggregate);
        }
    }
}
