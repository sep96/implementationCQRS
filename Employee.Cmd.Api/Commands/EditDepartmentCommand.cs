using CQRS.Core.Commands;

namespace Employee.Cmd.Api.Commands
{
    public class EditDepartmentCommand : BaseCommand
    {
        public string Deprtment { get; set; }
    }
}
