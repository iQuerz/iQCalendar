using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventsController : ControllerBase
    {
        CalendarContext _context;
        public EventsController(CalendarContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> getEvents()
        {
            return Ok(_context.Events);
        }

        [HttpPost]
        public async Task<ActionResult> postEvent([FromBody] Event @event)
        {
            await _context.Events.AddAsync(@event);
            await _context.SaveChangesAsync();
            return Ok(@event);
        }
    }
}
