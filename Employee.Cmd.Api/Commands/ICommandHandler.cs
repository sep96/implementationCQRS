namespace Employee.Cmd.Api.Commands
{
    public interface ICommandHandler
    {
        Task HandleAsync(NewEmployeeCommands command);
        Task HandleAsync(EditDepartmentCommand command);
        Task HandleAsync(DeleteEmployeeCommand command);
        Task HandleAsync(AddVacationCommand command);
    }
}
