using AutoMapper;
using implementationCQRS.Command;
using implementationCQRS.Dtos;
using implementationCQRS.Events;
using implementationCQRS.Models;
using implementationCQRS.Models.DbContext;
using MediatR;

namespace implementationCQRS.Handler
{
    //First Input Class Operate THE Request , Second Input Class Return Result 
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, EmployeeDTO>
    {
        readonly ApplicationDbContext _context;
        readonly IMapper _mapper;
        readonly IMediator _mediator;

        public CreateEmployeeCommandHandler(ApplicationDbContext context, IMapper mapper, IMediator mediator)
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<EmployeeDTO> Handle(CreateEmployeeCommand createEmployeeCommand, 
            CancellationToken cancellationToken)
        {

            Employee customer = _mapper.Map<Employee>(createEmployeeCommand);
            await _context.Employee.AddAsync(customer, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            // For testing PerformanceBehavior
            await Task.Delay(5000, cancellationToken);
            // Raising Event ...
            await _mediator.Publish(new EmployeeCreatedEvent(customer.FirstName, customer.LastName, customer.RegistrationDate), cancellationToken);
            return _mapper.Map<EmployeeDTO>(customer);
        }
    }
}
