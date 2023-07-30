using implementationCQRS.Events;
using MediatR;

namespace implementationCQRS.Handler
{
    public class EmployeeCreatedLoggerHandler : INotificationHandler<EmployeeCreatedEvent>
    {
        readonly ILogger<EmployeeCreatedLoggerHandler> _logger;
        public EmployeeCreatedLoggerHandler(ILogger<EmployeeCreatedLoggerHandler> logger)
        {
            _logger = logger;
        }
        public Task Handle(EmployeeCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"New Employee has been created at {notification.RegistrationDate}: " +
                $"{notification.FirstName} {notification.LastName}");

            return Task.CompletedTask;
        }
    }
}
