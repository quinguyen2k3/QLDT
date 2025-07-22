using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLDT.Dtos;
using QLDT.Dtos.request;
using QLDT.Dtos.response;
using QLDT.Manager;
using QLDT.Service;

namespace QLDT.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationSer _authenticationService;

        private readonly JwtManager _jwtHelper;

        public AuthenticationController(AuthenticationSer authenticationService, JwtManager jwtHelper)
        {
            _authenticationService = authenticationService;
            _jwtHelper = jwtHelper;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<AuthenticationRes>>> Login([FromBody] AuthenticationReq request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);

                return BadRequest(ApiResponse<AuthenticationRes>.ErrorResponse("Invalid request data", errors));
            }

            try
            {
                var result = await _authenticationService.AuthenticateAsync(request);

                if (!result.authenticated)
                {
                    if (!result.authenticated)
                    {
                        return Unauthorized(ApiResponse<AuthenticationRes>.ErrorResponse("Unauthenticated"));
                    }

                }

                return Ok(ApiResponse<AuthenticationRes>.SuccessResponse(result, "Login successful"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<AuthenticationRes>.ErrorResponse("Internal Server Error", new[] { ex.Message }));
            }
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

        [Authorize]
        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordReq request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);

                return BadRequest(ApiResponse<string>.ErrorResponse("Invalid request data", errors));
            }

            try
            {
                await _authenticationService.ChangePasswordAsync(request);
                return Ok(ApiResponse<string>.SuccessResponse(
                    null,
                    "Chang password successfully"
                ));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ApiResponse<string>.ErrorResponse(ex.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse("Internal Server Error", new[] { ex.Message }));
            }
        }

    }
}
