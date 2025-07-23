using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLDT.Attribute;
using QLDT.Dtos.request;
using QLDT.Dtos.response;
using QLDT.Service;

namespace QLDT.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserSer _service;

        public UserController(UserSer service)
        {
            _service = service;
        }

        [Authorize]
        [HasPermission("Report.ViewSummaryList")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<UserRes>>.SuccessResponse(
                data,
                "Fetched users successfully"
            ));
        }


        [Authorize]
        [HasPermission("User.ManageAccounts")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserReq request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);

                return BadRequest(ApiResponse<string>.ErrorResponse(
                    "Validation failed", errors
                ));
            }

            try
            {
                var created = await _service.CreateAsync(request);
                return CreatedAtAction(nameof(GetById), new { id = created.Id },
                    ApiResponse<UserRes>.SuccessResponse(
                        created,
                        "User created successfully"
                    ));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.ErrorResponse(
                    "System error", new[] { ex.Message }
                ));
            }
        }

        [Authorize]
        [HasPermission("User.ManageAccounts")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var data = await _service.GetByIdAsync(id);
            if (data == null)
            {
                return NotFound(ApiResponse<string>.ErrorResponse(
                    "User not found",
                    new[] { $"No user with ID {id}" }
                ));
            }

            return Ok(ApiResponse<UserRes>.SuccessResponse(
                data,
                "Fetched user successfully"
            ));
        }

        [Authorize]
        [HasPermission("User.ManageAccounts")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] UserReq request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);

                return BadRequest(ApiResponse<string>.ErrorResponse(
                    "Validation failed", errors
                ));
            }

            try
            {
                var updated = await _service.UpdateAsync(id, request);
                if (updated == null)
                {
                    return NotFound(ApiResponse<string>.ErrorResponse(
                        "User not found",
                        new[] { $"No user with ID {id}" }
                    ));
                }

                return Ok(ApiResponse<UserRes>.SuccessResponse(
                    updated,
                    "User updated successfully"
                ));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.ErrorResponse(
                    "System error", new[] { ex.Message }
                ));
            }
        }
    }
}
