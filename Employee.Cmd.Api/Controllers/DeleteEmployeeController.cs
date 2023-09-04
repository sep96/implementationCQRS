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
    public class DeleteEmployeeController : ControllerBase
    {
        private ILogger<DeleteEmployeeController> _logger;
        private ICommandDispatcher _commandDispatcher;

        public DeleteEmployeeController(ILogger<DeleteEmployeeController> logger, ICommandDispatcher commandDispatcher)
        {
            _logger = logger;
            _commandDispatcher = commandDispatcher;
        }
        [HttpDelete("{id}")]
        public virtual async Task<ActionResult> EditDepatrmentEmployeeAsync(Guid id, DeleteEmployeeCommand command)
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
