using Microsoft.AspNetCore.Mvc;

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
            catch(Exception e)
            {
                return BadRequest();
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
            catch(Exception e)
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
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
