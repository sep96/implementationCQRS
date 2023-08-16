using CQRS.Core.Commands;

namespace Employee.Cmd.Api.Commands
{
    public class DeleteEmployeeCommand : BaseCommand
    {
        public string Name { get; set; }
    }
}
