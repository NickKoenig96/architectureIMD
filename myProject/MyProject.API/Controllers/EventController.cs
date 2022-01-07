using System;
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
    [Route("events")]
    public class EventsController : ControllerBase
    {
        private readonly IDatabase _database;

        private readonly ILogger<EventsController> _logger;

        public EventsController(ILogger<EventsController> logger, IDatabase database)
        {
            _database = database;
            _logger = logger;
        }

        // get all events
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ViewEvent>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAllEvents() =>
            Ok((await _database.GetAllEvents())
                .Select(ViewEvent.FromModel).ToList());

        //get events by max age
        [HttpGet("/age/{eventage}")]
        [ProducesResponseType(typeof(IEnumerable<ViewEvent>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByAge(int eventage)
        {
            try
            {
                var eventAge = await _database.GetByAge(eventage);
                if (eventAge != null)
                {
                    return Ok(eventAge);

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

        //get event by id
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ViewEvent), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var eventId = await _database.GetEventById(Guid.Parse(id));
                if (eventId != null)
                {
                    return Ok(ViewEvent.FromModel(eventId));
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

        //edit and event
        [HttpPut()]
        [ProducesResponseType(typeof(ViewEvent), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Event>> UpdateEvent(UpdateEvent editEvent)
        {
            try
            {
                var createdEvent = editEvent.ToEvent();
                var persistedEvent = await _database.UpdateEvent(createdEvent);
                return CreatedAtAction(nameof(GetById), new { id = createdEvent.Id.ToString() }, ViewEvent.FromModel(persistedEvent));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Got an error for {nameof(UpdateEvent)}");
                return BadRequest(ex.Message);
            }
        }

        //create event
        [HttpPost("/create/event")]
        [ProducesResponseType(typeof(ViewEvent), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PersistEvent(CreateEvent postEvent)
        {
            try
            {
                _logger.LogInformation("new event created");
                var createdEvent = postEvent.ToEvent();
                var persistedEvent = await _database.PersistEvent(createdEvent);
                return CreatedAtAction(nameof(GetById), new { id = createdEvent.Id.ToString() }, ViewEvent.FromModel(persistedEvent));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //get enroll by id
        [HttpGet("/test{enrollId}")]
        [ProducesResponseType(typeof(ViewEroll), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetEnrollById(string id)
        {
            try
            {
                var enrollId = await _database.GetEnrollById(Guid.Parse(id));
                if (enrollId != null)
                {
                    return Ok(ViewEroll.FromModel(enrollId));
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

        //enroll user in event 
        [HttpPost("{eventTitle}/enroll/{userId}")]
        [ProducesResponseType(typeof(ViewEroll), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EnrollEvent(enrollEvent enrollEvent)
        {
            try
            {
                try
                {
                    _logger.LogInformation("enroll event");
                    var EnrolledEvent = enrollEvent.ToEnroll();
                    var persistedEnroll = await _database.EnrollEvent(EnrolledEvent);
                    // return Ok("user unenrolled");
                    return CreatedAtAction(nameof(GetById), new { id = EnrolledEvent.Id.ToString() }, ViewEroll.FromModel(persistedEnroll));

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //unenroll user in event
        [HttpDelete("{eventTitle}/unenroll/{userId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UnenrollEvent(string id)
        {
            try
            {
                var parsedId = Guid.Parse(id);
                var enroll = await _database.GetEnrollById(parsedId);
                if (enroll != null)
                {
                    await _database.UnenrollEvent(parsedId);
                    return NoContent();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Got an error for {nameof(UnenrollEvent)}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("enrolls")]
        [ProducesResponseType(typeof(IEnumerable<ViewEroll>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAllEnrolls() =>
            Ok((await _database.GetAllEnrolls())
                .Select(ViewEroll.FromModel).ToList());
    }
}