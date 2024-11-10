using Core.Managers;
using Microsoft.AspNetCore.Mvc;
using SeccionsProgamAPI.Models;

namespace SeccionsProgamAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ISessionManager _sessionManager;

        public AccountController(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }

        [HttpGet]
        public ActionResult<string> IsSessionActive(string sessionId)
        {
            try
            {
                var result = _sessionManager.IsSessionActive(sessionId);
                return Ok(result ? "Сессия активна" : "Сессия неактивна");
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message);
            }

        }

        [HttpPost]
        public ActionResult<string> AuthenticateUser(UserDTO user)
        {
            try
            {
                var result = _sessionManager.AuthenticateUser(user.Login, user.Password);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult DeleteSession(string sessionId)
        {
            try
            {
                var result = _sessionManager.DeleteSession(sessionId);

                if (result)
                {
                    return NoContent();
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
