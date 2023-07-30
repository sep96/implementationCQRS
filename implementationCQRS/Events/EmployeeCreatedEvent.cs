using MediatR;

namespace implementationCQRS.Events
{
    public class EmployeeCreatedEvent : INotification
    {
        public EmployeeCreatedEvent(string firstName, string lastName, DateTime registrationDate)
        {
            FirstName = firstName;
            LastName = lastName;
            RegistrationDate = registrationDate;
        }
        public string FirstName { get; }

        public string LastName { get; }

        public DateTime RegistrationDate { get; }
    }
}
