using CQRS.Core.Infrastuctur;
using Employee.Cmd.Api.Commands;
using Employee.Cmd.Api.Dtos;
using Employee.Common.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Employee.Cmd.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddVacationController : ControllerBase
    {
        private ILogger<AddVacationController> _logger;
        private ICommandDispatcher _commandDispatcher;

        public AddVacationController(ILogger<AddVacationController> logger, ICommandDispatcher commandDispatcher)
        {
            _logger = logger;
            _commandDispatcher = commandDispatcher;
        }
        [HttpPatch("{id}")]
        public virtual async Task<ActionResult> AddVacationCommandAsync(Guid id, AddVacationCommand command)
        {
            command.Id = id;
            try
            {
                await _commandDispatcher.SendAsync(command);
                return StatusCode(StatusCodes.Status200OK, new NewEmployeeResponse
                {
                    Id = command.Id,
                    Message = "Employee Deleted Successfully"
                });
            }
            catch (InvalidOperationException sd)
            {
                _logger.Log(LogLevel.Warning, "Client had bad request");
                return BadRequest(new BaseResponse
                {
                    Message = sd.Message
                });
            }
            catch (Exception ex)
            {
                const string SAFE_ERROR_MESSSAGE = "internal Error";
                _logger.Log(LogLevel.Error, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new NewEmployeeResponse
                {
                    Message = SAFE_ERROR_MESSSAGE
                    ,
                    Id = id
                });
            }
        }
    }
}
