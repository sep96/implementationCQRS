using CQRS.Core.Commands;

namespace Employee.Cmd.Api.Commands
{
    public class AddVacationCommand : BaseCommand
    {
        public int TotalDays { get; set; }

        public DateTime StartDate { get; set; }
    }
}
