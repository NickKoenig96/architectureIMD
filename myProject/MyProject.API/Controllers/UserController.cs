using System;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using MyProject.API.Domain;
using System.Threading.Tasks;
using MyProject.API.Ports;



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

        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var User = await _database.GetEventById(Guid.Parse(id));
                if (User != null)
                {
                    return Ok(User);
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
        public async Task<IActionResult> PersistUser(CreateUser User)
        {
            try
            {
                _logger.LogInformation($"Cool, creating a new user");
                var createdUser = User.ToUser();
                var persistedUser = await _database.PersistUser(createdUser);
                return CreatedAtAction(nameof(GetById), new { id = createdUser.Id.ToString() }, ViewUser.FromModel(persistedUser));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}



