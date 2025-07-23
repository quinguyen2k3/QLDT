using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLDT.Attribute;
using QLDT.Dtos.request;
using QLDT.Dtos.response;
using QLDT.Service;

namespace QLDT.Controllers
{
    [Route("api/major")]
    [ApiController]
    public class MajorController : ControllerBase
    {
        private readonly MajorSer _service;

        public MajorController(MajorSer service)
        {
            _service = service;
        }

        [Authorize]
        [HasPermission("Report.ViewSummaryList")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<MajorRes>>.SuccessResponse(
                data,
                "Fetched majors successfully"
            ));
        }

        [Authorize]
        [HasPermission("Report.ViewActiveList")]
        [HttpGet("active")]
        public async Task<IActionResult> GetAllActive()
        {
            var data = await _service.GetAllActiveAsync();
            return Ok(ApiResponse<IEnumerable<MajorRes>>.SuccessResponse(
                data,
                "Fetched majors successfully"
            ));
        }

        [Authorize]
        [HasPermission("Report.ViewOwnList")]
        [HttpGet("me")]
        public async Task<IActionResult> GetAllByMe()
        {
            var data = await _service.GetAllByUserAsync();
            return Ok(ApiResponse<IEnumerable<MajorRes>>.SuccessResponse(
                data,
                "Fetched majors successfully"
            ));
        }

        [Authorize]
        [HasPermission("Major.Manage")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MajorReq request)
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
                    ApiResponse<MajorRes>.SuccessResponse(
                        created,
                        "Major created successfully"
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
        [HasPermission("Major.Manage")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var data = await _service.GetByIdAsync(id);
            if (data == null)
            {
                return NotFound(ApiResponse<string>.ErrorResponse(
                    "Not found", new[] { $"No major with ID {id}" }
                ));
            }

            return Ok(ApiResponse<MajorRes>.SuccessResponse(
                data, "Fetched major successfully"
            ));
        }

        [Authorize]
        [HasPermission("Major.Manage")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] MajorReq request)
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
                        "Not found", new[] { $"No major with ID {id}" }
                    ));
                }

                return Ok(ApiResponse<MajorRes>.SuccessResponse(
                    updated, "Major updated successfully"
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
