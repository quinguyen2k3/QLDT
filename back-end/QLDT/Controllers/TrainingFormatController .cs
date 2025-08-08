using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLDT.Attribute;
using QLDT.Dtos.request;
using QLDT.Dtos.response;
using QLDT.Service;

namespace QLDT.Controllers
{
    [Route("api/format")]
    [ApiController]
    public class TrainingFormatController : ControllerBase
    {
        private readonly TrainingFormatSer _service;

        public TrainingFormatController(TrainingFormatSer service)
        {
            _service = service;
        }

        [Authorize]
        [HasPermission("Report.ViewSummaryList")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<TrainingFormatRes>>.SuccessResponse(
                data,
                "Fetched training formats successfully"
            ));
        }

        [Authorize]
        [HasPermission("Report.ViewActiveList")]
        [HttpGet("active")]
        public async Task<IActionResult> GetAllActive()
        {
            var data = await _service.GetAllActiveAsync();
            return Ok(ApiResponse<IEnumerable<TrainingFormatRes>>.SuccessResponse(
                data,
                "Fetched training formats successfully"
            ));
        }

        [Authorize]
        [HasPermission("TrainingFormat.Manage")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TrainingFormatReq request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);

                return BadRequest(ApiResponse<string>.ErrorResponse(
                    "Validation failed",
                    errors
                ));
            }

            try
            {
                var created = await _service.CreateAsync(request);

                return CreatedAtAction(nameof(GetById), new { id = created.Id },
                    ApiResponse<TrainingFormatRes>.SuccessResponse(
                        created,
                        "Training format created successfully"
                    ));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.ErrorResponse(
                    "System error",
                    new[] { ex.Message }
                ));
            }
        }


        [Authorize]
        [HasPermission("TrainingFormat.Manage")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var data = await _service.GetByIdAsync(id);
            if (data == null)
            {
                return NotFound(ApiResponse<string>.ErrorResponse(
                    "Training format not found",
                    new[] { $"No training format with ID {id} was found." }
                ));
            }

            return Ok(ApiResponse<TrainingFormatRes>.SuccessResponse(
                data,
                "Fetched training format successfully"
            ));
        }

        [Authorize]
        [HasPermission("TrainingFormat.Manage")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] TrainingFormatReq request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);

                return BadRequest(ApiResponse<string>.ErrorResponse(
                    "Validation failed",
                    errors
                ));
            }

            try
            {
                var updated = await _service.UpdateAsync(id, request);
                if (updated == null)
                {
                    return NotFound(ApiResponse<string>.ErrorResponse(
                        "Training format not found",
                        new[] { $"No training format with ID {id} was found." }
                    ));
                }

                return Ok(ApiResponse<TrainingFormatRes>.SuccessResponse(
                    updated,
                    "Training format updated successfully"
                ));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.ErrorResponse(
                    "System error",
                    new[] { ex.Message }
                ));
            }
        }

        [Authorize]
        [HasPermission("Report.ViewActiveList")]
        [HttpGet("basic")]
        public async Task<IActionResult> GetBasic()
        {
            var data = await _service.GetFormat1And2Async();
            return Ok(ApiResponse<IEnumerable<TrainingFormatRes>>.SuccessResponse(
                data,
                "Fetched training formats successfully"
            ));
        }
    }
}