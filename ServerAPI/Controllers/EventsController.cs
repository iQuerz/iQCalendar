using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

using ServerAPI.Business;
using ServerAPI.Data;
using ServerAPI.Data.Models;
using ServerAPI.Logs;

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
            catch(iQException e)
            {
                return StatusCode(e.StatusCode, e.Error);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> postEvent([FromBody] Event @event)
        {
            if (!await _logic.AuthenticateAdmin(Request))
                return Unauthorized();

            try
            {
                var result = Ok(await _logic.CreateEvent(@event));
                iQLogger.addLog(Request, @event);
                return result;
            }
            catch (iQException e)
            {
                return StatusCode(e.StatusCode, e.Error);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> putEvent([FromBody] Event @event)
        {
            if (!await _logic.AuthenticateAdmin(Request))
                return Unauthorized();

            try
            {
                var result = Ok(await _logic.UpdateEvent(@event));
                iQLogger.addLog(Request, @event);
                return result;
            }
            catch (iQException e)
            {
                return StatusCode(e.StatusCode, e.Error);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("{eventID}")]
        public async Task<ActionResult> deleteEvent(int eventID)
        {
            if (!await _logic.AuthenticateAdmin(Request))
                return Unauthorized();

            try
            {
                var result = _logic.DeleteEvent(eventID);
                iQLogger.addLog(Request);
                return Ok();
            }
            catch (iQException e)
            {
                return StatusCode(e.StatusCode, e.Error);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
