using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLDT.Dtos;
using QLDT.Dtos.request;
using QLDT.Dtos.response;
using QLDT.Helper;
using QLDT.Service;

namespace QLDT.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationSer _authenticationService;

        private readonly JwtHelper _jwtHelper;

        public AuthenticationController(AuthenticationSer authenticationService, JwtHelper jwtHelper)
        {
            _authenticationService = authenticationService;
            _jwtHelper = jwtHelper;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthenticationRes>> Login([FromBody] AuthenticationReq request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authenticationService.AuthenticateAsync(request);

            if (!result.authenticated)
            {
                return Unauthorized(new { message = "Unauthenticated" });
            }

            return Ok(result);
        }

        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken([FromBody] TokenDto request)
        {
            if (request == null || string.IsNullOrEmpty(request.accessToken) || string.IsNullOrEmpty(request.refreshToken))
            {
                return BadRequest(new { message = "AccessToken and RefreshToken are required." });
            }

            try
            {
                var tokenDto = await _jwtHelper.RenewAccessTokenAsync(request);
                return Ok(tokenDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] TokenDto request)
        {       
            try
            {
                await _authenticationService.LogoutAsync(request);

                return Ok(new { message = "Logout successful." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
