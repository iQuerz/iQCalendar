using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using ServerAPI.Business;
using ServerAPI.Data;
using ServerAPI.Data.Models;
using ServerAPI.Exceptions;
using ServerAPI.Logs;

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
                var result = Ok(await _logic.CreateAccount(account));
                iQLogger.addLog(Request, account);
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
                var result = StatusCode(e.StatusCode, e.Error);
                iQLogger.addLog(Request, account);
                return result;
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
