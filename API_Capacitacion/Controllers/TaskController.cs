using Microsoft.AspNetCore.Mvc;
using WebApi.Data.Interfaces;
using WebApi.DTOs.Task;
using WebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Capacitacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        // GET: api/<TaskController>
        ITaskService _service;

        public TaskController(ITaskService service) => _service = service;

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<TaskController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TaskController>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaskDTO createTaskDTO)
        {
            TaskModel? task = await _service.Create(createTaskDTO);

            if (task == null) return NotFound();

            return Ok(task);
        }

        // PUT api/<TaskController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TaskController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
