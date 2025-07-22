using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLDT.Dtos.request;
using QLDT.Dtos.response;
using QLDT.Service;

namespace QLDT.Controllers
{
    [Route("api/education-level")]
    [ApiController]
    public class EducationLevelController : ControllerBase
    {
        private readonly EducationLevelSer _service;

        public EducationLevelController(EducationLevelSer service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<EducationLevelRes>>.SuccessResponse(
                data,
                "Fetched education levels successfully"
            ));
        }

        [Authorize]
        [HttpGet("active")]
        public async Task<IActionResult> GetAllActive()
        {
            var data = await _service.GetAllActiveAsync();
            return Ok(ApiResponse<IEnumerable<EducationLevelRes>>.SuccessResponse(
                data,
                "Fetched education levels successfully"
            ));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EducationLevelReq request)
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
                    ApiResponse<EducationLevelRes>.SuccessResponse(
                        created,
                        "Education level created successfully"
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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var data = await _service.GetByIdAsync(id);
            if (data == null)
            {
                return NotFound(ApiResponse<string>.ErrorResponse(
                    "Not found", new[] { $"No education level with ID {id}" }
                ));
            }

            return Ok(ApiResponse<EducationLevelRes>.SuccessResponse(
                data, "Fetched education level successfully"
            ));
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] EducationLevelReq request)
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
                        "Not found", new[] { $"No education level with ID {id}" }
                    ));
                }

                return Ok(ApiResponse<EducationLevelRes>.SuccessResponse(
                    updated, "Education level updated successfully"
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
