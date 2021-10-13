using System;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyProject.API.Domain;



namespace MyProject.API.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger) => _logger = logger;



        //get events by age
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult GetById(string id)
        {
            try
            {
                var User = EventProvider.StaticEventList.FirstOrDefault(x => x.Id == Guid.Parse(id));
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


        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateUser(CreateUser user)
        {
            try
            {
                _logger.LogInformation($"Cool, creating a new user");
                var createdUser = new User(Guid.NewGuid(), user.userName, user.userBirthdate, user.userEmail);
                return CreatedAtAction(nameof(GetById), new { id = createdUser.Id.ToString() }, createdUser);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}



