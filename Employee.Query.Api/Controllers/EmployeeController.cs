using CQRS.Core.Infrastuctur;
using Employee.Query.Api.Dtos;
using Employee.Query.Api.Queries;
using Employee.Query.Domain.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Employee.Query.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IQueryDispatcher<EmployeeEntity> _query;

        public EmployeeController(ILogger<EmployeeController> logger, IQueryDispatcher<EmployeeEntity> query)
        {
            _logger = logger;
            _query = query;
        }
        [HttpGet]
        public virtual async Task<ActionResult<List<EmployeeEntity>>> getEmployee()
        => Ok(await _query.SendAsync(new FindAllEmployeeQuery()));

        [HttpGet("{EmployeeId}")]
        public virtual async Task<ActionResult<List<EmployeeEntity>>> getEmployeeById(Guid EmployeeId)
        => Ok(await _query.SendAsync(new FindbyIdEmployee { ID = EmployeeId }));
    }
}
