using CQRS.Core.Commands;

namespace Employee.Cmd.Api.Commands
{
    public class NewEmployeeCommands : BaseCommand
    {
        public string Name { get; set; }
        public string Department { get; set; }
    }
}
