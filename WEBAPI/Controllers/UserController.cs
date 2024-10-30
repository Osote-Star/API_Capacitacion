using Microsoft.AspNetCore.Mvc;
using System.Collections;
using WEBAPI.Data.Interfaces;
using WEBAPI.DTOs.Tarea;
using WEBAPI.DTOs.User;
using WEBAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _service;

        public UserController(IUserService service) => _service = service;
        // GET: api/<UserController>
        [HttpGet]
        public async Task<IActionResult> FindAll()
        {
            IEnumerable<UserModel> users = await _service.FindAll();
            return Ok(users); 
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            UserModel? user = await _service.FindOne(id);
            if (user == null)  return NotFound();
            return Ok(user);
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserDto createUserDto)
        {
            UserModel? user = await _service.Create(createUserDto);

            return Ok(user);
        }

        // PUT api/<UserController>/5
        [HttpPut("{userID}")]
        public async Task<IActionResult> Put(int userID,UpdateUserDto updateUserDto)
        {
            UserModel? user = await _service.Update(userID,updateUserDto);

            if (user == null) return NotFound();

            return Ok(user);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            UserModel? user = await _service.Remove(id);

            if (user == null) return NotFound();

            return Ok(user);
        }
    }
}
