using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLDT.Attribute;
using QLDT.Dtos.request;
using QLDT.Dtos.response;
using QLDT.Service;

namespace QLDT.Controllers
{
    [Route("api/credit-hourse")]
    [ApiController]
    public class CreditHourseController : ControllerBase
    {
        private readonly CreditHourseSer _service;

        public CreditHourseController(CreditHourseSer service)
        {
            _service = service;
        }

        [Authorize]
        [HasPermission("Report.ViewSummaryList")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<CreditHourseRes>>.SuccessResponse(
                data,
                "Fetched credit hour successfully"
            ));
        }

        [Authorize]
        [HasPermission("Report.ViewActiveList")]
        [HttpGet("active")]
        public async Task<IActionResult> GetAllActive()
        {
            var data = await _service.GetAllActiveAsync();
            return Ok(ApiResponse<IEnumerable<CreditHourseRes>>.SuccessResponse(
                data,
                "Fetched credit hourse successfully"
            ));
        }

        [Authorize]
        [HasPermission("CreditHourse.Manage")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreditHourseReq request)
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
                    ApiResponse<CreditHourseRes>.SuccessResponse(
                        created,
                        "Credit hourse created successfully"
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
        [HasPermission("CreditHourse.Manage")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var data = await _service.GetByIdAsync(id);
            if (data == null)
            {
                return NotFound(ApiResponse<string>.ErrorResponse(
                    "Not found", new[] { $"No credit hour with ID {id}" }
                ));
            }

            return Ok(ApiResponse<CreditHourseRes>.SuccessResponse(
                data, "Fetched credit hour successfully"
            ));
        }

        [Authorize]
        [HasPermission("CreditHourse.Manage")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] CreditHourseReq request)
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
                        "Not found", new[] { $"No credit hour with ID {id}" }
                    ));
                }

                return Ok(ApiResponse<CreditHourseRes>.SuccessResponse(
                    updated, "Credit hours updated successfully"
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
