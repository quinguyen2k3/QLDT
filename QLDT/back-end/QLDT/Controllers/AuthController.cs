using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLDT.Dtos.Request;
using QLDT.Service;

namespace QLDT.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthSer _ser;
        public AuthController(IAuthSer ser) => _ser = ser;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginReq req)
            => Ok(await _ser.LoginAsync(req));

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _ser.LogoutAsync();
            return NoContent();
        }

        [Authorize]
        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordReq req)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (!await _ser.ChangePasswordAsync(userId, req))
                return BadRequest("Old password is incorrect");
            return NoContent();
        }
    }
}
