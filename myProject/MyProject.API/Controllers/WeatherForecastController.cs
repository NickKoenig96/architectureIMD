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
    [Route("events")]
    public class EventsController : ControllerBase
    {
        private readonly ILogger<EventsController> _logger;

        public EventsController(ILogger<EventsController> logger) => _logger = logger;

        /*[HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get(string titleStartsWith) => Ok(EventProvider.StaticEventList.FirstOrDefault(x => x.eventTitle.StartsWith(titleStartsWith ?? string.Empty, true, CultureInfo.InvariantCulture)));
*/
        // get all events
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult Get() => Ok(EventProvider.StaticEventList);

        //get events by age
        [HttpGet("{eventage}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult GetById(int eventage)
        {
            try
            {
                var Event = EventProvider.StaticEventList.FirstOrDefault(x => x.eventAge == eventage);
                if (Event != null)
                {
                    return Ok(Event);
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


        //werkt nog niet
        /* [HttpPut("{id}")]
         [ProducesResponseType(StatusCodes.Status200OK)]
         [ProducesResponseType(StatusCodes.Status404NotFound)]
         [ProducesResponseType(StatusCodes.Status400BadRequest)]

         public IActionResult CreateEvent(CreateEvent Event)
         {
             try
             {
                 _logger.LogInformation($"Cool, creating a new movie");
                 var createdEvent = Event.ToEvent();
                 return CreatedAtAction(nameof(GetById), new { id = createdEvent.Id.ToString() });
             }
             catch (Exception ex)
             {
                 return BadRequest(ex.Message);
             }


         }*/
    }





}





