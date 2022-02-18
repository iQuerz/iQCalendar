using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

using ServerAPI.Business;
using ServerAPI.Data;
using ServerAPI.Data.Models;

namespace ServerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        Logic _logic;
        public AccountsController(CalendarContext context)
        {
            _logic = new Logic(context);
        }

        [HttpGet]
        [Route("{username}")]
        public async Task<ActionResult> getAccount(string username)
        {
            if (!await _logic.Authenticate(Request))
                return Unauthorized();

            try
            {
                return Ok(await _logic.GetAccount(username));
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

        [HttpGet]
        [Route("{name}")]
        public async Task<ActionResult> getAccounts(string name)
        {
            if (!await _logic.AuthenticateServer(Request))
                return Unauthorized();

            try
            {
                return Ok(await _logic.GetAccounts(name));
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

        [HttpPost]
        public async Task<ActionResult> createAccount([FromBody]Account account)
        {
            try
            {
                return Ok(await _logic.CreateAccount(account));
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
        public async Task<ActionResult> updateAccount([FromBody] Account account)
        {
            if (!await _logic.AuthenticateAdmin(Request))
                return Unauthorized();

            try
            {
                return Ok(await _logic.UpdateAccount(account));
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
        [Route("{accountID}")]
        public async Task<ActionResult> deleteAccount(int accountID)
        {
            if (!await _logic.AuthenticateAdmin(Request))
                return Unauthorized();

            try
            {
                await _logic.DeleteAccount(accountID);
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
