using implementationCQRS.Events;
using MediatR;

namespace implementationCQRS.Handler
{
    public class EmployeeCreatedEmailSenderHandler : INotificationHandler<EmployeeCreatedEvent>
    {
        public Task Handle(EmployeeCreatedEvent notification, CancellationToken cancellationToken)
        {
            // IMessageSender.Send($"Welcome {notification.FirstName} {notification.LastName} !");
            return Task.CompletedTask;
        }
    }
}
