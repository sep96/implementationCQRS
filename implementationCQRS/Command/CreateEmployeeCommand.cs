using implementationCQRS.Dtos;
using MediatR;

namespace implementationCQRS.Command
{
    public class CreateEmployeeCommand : IRequest<EmployeeDTO>
    {
        public CreateEmployeeCommand(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; }

        public string LastName { get; }
    }
}
