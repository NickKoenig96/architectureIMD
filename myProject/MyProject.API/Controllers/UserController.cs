using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyProject.API.Ports;
using MyProject.API.Domain;

namespace MyProject.API.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly IDatabase _database;

        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, IDatabase database)
        {
            _database = database;
            _logger = logger;
        }

        //get participants
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ViewUser>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public async Task<IActionResult> Get() =>
           Ok((await _database.GetAllUsers())
               .Select(ViewUser.FromModel).ToList());

        //get user by id
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ViewUser), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> GetUserById(string id)
        {
            try
            {
                var user = await _database.GetUserById(Guid.Parse(id));
                if (user != null)
                {
                    return Ok(user);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //register user
        [HttpPost("/create/user")]
        [ProducesResponseType(typeof(ViewUser), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PersistUser(CreateUser user)
        {
            try
            {
                _logger.LogInformation("new user created");
                var createdUser = user.ToUser();
                var persistedUser = await _database.PersistUser(createdUser);
                return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id.ToString() }, ViewUser.FromModel(persistedUser));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //delete user
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveUser(string id)
        {
            try
            {
                var parsedId = Guid.Parse(id);
                var user = await _database.GetUserById(parsedId);
                if (user != null)
                {
                    await _database.DeleteUser(parsedId);
                    return NoContent();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Got an error for {nameof(RemoveUser)}");
                return BadRequest(ex.Message);
            }
        }
    }
}