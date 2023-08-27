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
    public class NewPostController : ControllerBase
    {
        private readonly ILogger<NewPostController> _logger;
        private readonly ICommandDispatcher _commandDispatcher;

        public NewPostController(ILogger<NewPostController> logger, ICommandDispatcher commandDispatcher)
        {
            _logger = logger;
            _commandDispatcher = commandDispatcher;
        }
        [HttpPost]
        public virtual async Task<ActionResult> NewPost(NewEmployeeCommands command)
        {
                var id = Guid.NewGuid();
            try
            {
                command.Id = id;
                await _commandDispatcher.SendAsync(command);
                return StatusCode(StatusCodes.Status200OK, new NewEmployeeResponse
                {
                    Message = "Employee Created Successfully"
                });
            }
            catch(InvalidOperationException sd)
            {
                _logger.Log(LogLevel.Warning, "Cient mad bad request");
                return BadRequest(new BaseResponse
                {
                    Message = sd.Message
                });
            }
            catch(Exception ex) 
            {
                const string SAFE_ERROR_MESSSAGE = "internal Error";
                _logger.Log(LogLevel.Error, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError , new NewEmployeeResponse
                {
                    Message = SAFE_ERROR_MESSSAGE
                    ,Id = id
                });
            }
        }
    }
}
