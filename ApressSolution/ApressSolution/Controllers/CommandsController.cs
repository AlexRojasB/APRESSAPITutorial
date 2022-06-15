using ApressSolution.Data;
using ApressSolution.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApressSolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandAPIRepo _repository;
        public CommandsController(ICommandAPIRepo repository)
        {
            _repository = repository;
        }
    
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var commandItems = _repository.GetAllCommands();
            return Ok(commandItems);
        }

        [HttpGet("{id}")]
        public ActionResult<Command> GetCommandById(int id)
        {
            var command = _repository.GetCommandById(id);
            if (command == null)
                return NotFound();
            return Ok(command);
        }
    }
}
