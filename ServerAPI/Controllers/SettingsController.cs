using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

using ServerAPI.Business;
using ServerAPI.Data;
using ServerAPI.Data.Models;
using ServerAPI.Exceptions;

namespace ServerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SettingsController : Controller
    {
        private readonly IHostApplicationLifetime _applicationLifetime;
        Logic _logic;
        public SettingsController(CalendarContext context, IHostApplicationLifetime appLT)
        {
            _logic = new Logic(context);
            _applicationLifetime = appLT;
        }

        [HttpGet]
        [Route("{name}")]
        public async Task<ActionResult> getSettings(string name)
        {
            if (!await _logic.AuthenticateServer(Request))
                return Unauthorized();

            try
            {
                return Ok(await _logic.GetSettings(name));
            }
            catch (iQException e)
            {
                return StatusCode(e.StatusCode, e.Error);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> updateSettings([FromBody] Settings settings)
        {

            if (!await _logic.AuthenticateServer(Request))
                return Unauthorized();

            try
            {
                return Ok(await _logic.UpdateSettings(settings));
            }
            catch (iQException e)
            {
                return StatusCode(e.StatusCode, e.Error);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("stop/{name}")]
        public async Task<ActionResult> Stop(string name)
        {
            if (!await _logic.AuthenticateServer(Request))
                return Unauthorized();

            try
            {
                _applicationLifetime.StopApplication();
                return Ok("Server stopped.");
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
