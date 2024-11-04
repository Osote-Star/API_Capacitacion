using Microsoft.AspNetCore.Mvc;
using WEBAPI.Data.Interfaces;
using WEBAPI.DTOs.Tarea;
using WEBAPI.DTOs.User;
using WEBAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareaController : ControllerBase
    {
        ITareaService _service;

        public TareaController (ITareaService service) => _service = service;

        // GET: api/<TareaController>
        [HttpGet]
        public async Task<IActionResult> FindAll(int userID)
        {
            UserModel? user = await _service.FindAll(userID);
            if (user == null) return NotFound();
            return Ok(user);
        }

        // POST api/<TareaController>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTareaDto
            createTareaDto) {
            TareaModel? task = await _service.Create(createTareaDto);

            if (task == null) return NotFound();

            return Ok(task);
        }

        // PUT api/<TareaController>/5
        [HttpPut("{idtask}")]
        public async Task<IActionResult> Put(int idtask, UpdateTareaDto updateTareaDto)
        {
            TareaModel? task = await _service.Update(idtask, updateTareaDto);

            if (task == null) return NotFound();

            return Ok(task);
        }

        // DELETE api/<TareaController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            TareaModel? task = await _service.Remove(id);

            if (task == null) return NotFound();

            return Ok(task);
        }
    }
}
