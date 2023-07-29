using AutoMapper;
using implementationCQRS.Command;
using implementationCQRS.Dtos;
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
      
        public CreateEmployeeCommandHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<EmployeeDTO> Handle(CreateEmployeeCommand createEmployeeCommand, 
            CancellationToken cancellationToken)
        {

            Employee customer = _mapper.Map<Employee>(createEmployeeCommand);
            await _context.Employee.AddAsync(customer, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<EmployeeDTO>(customer);
        }
    }
}
