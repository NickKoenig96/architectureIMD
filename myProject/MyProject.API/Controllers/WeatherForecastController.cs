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
        //public EventsController(ILogger<EventsController> logger) => _logger = logger;


        // get all events
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ViewEvent>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        // public async Task<IActionResult> Get() => Ok(EventProvider.StaticEventList);

        public async Task<IActionResult> Get() =>
            Ok((await _database.GetAllEvents())
                .Select(ViewEvent.FromModel).ToList());


        //get events by age
        [HttpGet("age/{eventage}")]
        [ProducesResponseType(typeof(IEnumerable<ViewEvent>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> GetByAge(int eventage)
        {
            try
            {
                var Event = await _database.GetByAge(eventage);
                if (Event != null)
                {
                    //return Ok(Event);
                    return Ok(Event);
                    //return Ok(ViewEvent.FromModel(Event));

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


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ViewEvent), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var Event = await _database.GetEventById(Guid.Parse(id));
                if (Event != null)
                {
                    return Ok(ViewEvent.FromModel(Event));
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                // This is just good practice; you never want to expose a raw exception message. There are some libraries/services to handle this
                // but it's better to take full control of your code.
                return BadRequest(ex.Message);
            }
        }


        /* [HttpPost()]
         [ProducesResponseType(typeof(ViewEvent), StatusCodes.Status201Created)]
         [ProducesResponseType(StatusCodes.Status201Created)]
         [ProducesResponseType(StatusCodes.Status400BadRequest)]
         public async Task<IActionResult> PersistEvent(CreateEvent User)
         {
             try
             {
                 _logger.LogInformation($"Cool, creating a new user");
                 var createdEvent = User.ToEvent();
                 var persistedEvent = await _database.PersistEvent(createdEvent);
                 return CreatedAtAction(nameof(GetById), new { id = createdEvent.Id.ToString() }, ViewEvent.FromModel(persistedEvent));
             }
             catch (Exception ex)
             {
                 return BadRequest(ex.Message);
             }
         }*/

        //werkt nog niet
        /* [HttpPut("/edit")]
         [ProducesResponseType(typeof(CreateEvent), StatusCodes.Status201Created)]
         [ProducesResponseType(StatusCodes.Status400BadRequest)]
         public async Task<IActionResult> PersistEvent(CreateEvent Event)
         {
             try
             {
                 var createdEvent = Event.ToEvent();
                 var persistedEvent = await _database.PersistEvent(createdEvent);
                 return CreatedAtAction(nameof(GetById), new { id = createdEvent.Id.ToString() }, ViewEvent.FromModel(persistedEvent));

             }
             catch (Exception ex)
             {
                 _logger.LogError(ex, $"Got an error for {nameof(PersistEvent)}");
                 return BadRequest(ex.Message);
             }
         }*/


        [HttpPost("/make")]
        [ProducesResponseType(typeof(ViewEvent), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PersistEvent(CreateEvent Event)
        {
            try
            {
                _logger.LogInformation($"Cool, creating a new event");
                var createdEvent = Event.ToEvent();
                var persistedEvent = await _database.PersistEvent(createdEvent);
                return CreatedAtAction(nameof(GetById), new { id = createdEvent.Id.ToString() }, ViewEvent.FromModel(persistedEvent));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }





}





