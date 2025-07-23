using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLDT.Attribute;
using QLDT.Dtos.request;
using QLDT.Dtos.response;
using QLDT.Service;

namespace QLDT.Controllers
{
    [Route("api/training-unit")]
    [ApiController]
    public class TrainingUnitController : ControllerBase
    {
        private readonly TrainingUnitSer _service;

        public TrainingUnitController(TrainingUnitSer service)
        {
            _service = service;
        }

        [Authorize]
        [HasPermission("TrainingUnit.Manage")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<TrainingUnitRes>>.SuccessResponse(
                data,
                "Fetched training units successfully"
            ));
        }

        [Authorize]
        [HasPermission("Report.ViewActiveList")]
        [HttpGet("active")]
        public async Task<IActionResult> GetAllActive()
        {
            var data = await _service.GetAllActiveAsync();
            return Ok(ApiResponse<IEnumerable<TrainingUnitRes>>.SuccessResponse(
                data,
                "Fetched training units successfully"
            ));
        }

        [Authorize]
        [HasPermission("TrainingUnit.Manage")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TrainingUnitReq request)
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
                    ApiResponse<TrainingUnitRes>.SuccessResponse(
                        created,
                        "Training unit created successfully"
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
        [HasPermission("TrainingUnit.Manage")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var data = await _service.GetByIdAsync(id);
            if (data == null)
            {
                return NotFound(ApiResponse<string>.ErrorResponse(
                    "Not found", new[] { $"No training unit with ID {id}" }
                ));
            }

            return Ok(ApiResponse<TrainingUnitRes>.SuccessResponse(
                data, "Fetched training unit successfully"
            ));
        }

        [Authorize]
        [HasPermission("TrainingUnit.Manage")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] TrainingUnitReq request)
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
                        "Not found", new[] { $"No training unit with ID {id}" }
                    ));
                }

                return Ok(ApiResponse<TrainingUnitRes>.SuccessResponse(
                    updated, "Training unit updated successfully"
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
