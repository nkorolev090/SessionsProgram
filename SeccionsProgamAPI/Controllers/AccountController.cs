using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SeccionsProgamAPI.Models;

namespace SeccionsProgamAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ISessionService _sessionService;

        public AccountController(ISessionService sessionManager)
        {
            _sessionService = sessionManager;
        }

        [HttpGet("isSessionActive")]
        public ActionResult<string> IsSessionActive(string sessionId)
        {
            try
            {
                var result = _sessionService.IsSessionActive(sessionId);
                return Ok(result ? "Сессия активна" : "Сессия неактивна");
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message);
            }

        }

        [HttpPost("authenticateUser")]
        public async Task<ActionResult<string>> AuthenticateUser(UserDTO user)
        {
            try
            {
                var result = await _sessionService.AuthenticateUser(user.Login, user.Password);

                if(result == null)
                {
                    return Unauthorized();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message);
            }
        }

        [HttpDelete("deleteSession")]
        public IActionResult DeleteSession(string sessionId)
        {
            try
            {
                var result = _sessionService.DeleteSession(sessionId);

                if (result)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message);
            }
        }
    }
}
