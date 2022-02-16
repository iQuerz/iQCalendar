using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServerAPI.Business;
using ServerAPI.Data;
using ServerAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        Logic _logic;
        public EventsController(CalendarContext context)
        {
            _logic = new Logic(context);
        }

        [HttpGet]
        [Route("{accountID}/{month}/{year}")]
        public async Task<ActionResult> getEventsByMonth(int accountID, int month, int year)
        {
            if (!await _logic.Authenticate(Request))
                return Unauthorized();
            try
            {
                return Ok(await _logic.GetMonthlyEvents(accountID, month, year));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> postEvent([FromBody] Event @event)
        {
            try
            {
                return Ok(await _logic.CreateEvent(@event));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> putEvent([FromBody] Event @event)
        {
            try
            {
                return Ok(await _logic.UpdateEvent(@event));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("{eventID}")]
        public async Task<ActionResult> deleteEvent(int eventID)
        {
            if (!await _logic.Authenticate(Request))
                return Unauthorized();

            try
            {
                await _logic.DeleteEvent(eventID);
                return Ok();
            }
            catch(iQException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
