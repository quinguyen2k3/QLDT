using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLDT.Dtos.request;
using QLDT.Dtos.response;
using QLDT.Service;

namespace QLDT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeSer _service;

        public EmployeeController(EmployeeSer service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<EmployeeRes>>.SuccessResponse(
                data,
                "Fetched employees successfully"
            ));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmployeeReq request)
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
                    ApiResponse<EmployeeRes>.SuccessResponse(
                        created,
                        "Employee created successfully"
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
                    "Employee not found",
                    new[] { $"No employee with ID {id}" }
                ));
            }

            return Ok(ApiResponse<EmployeeRes>.SuccessResponse(
                data,
                "Fetched employee successfully"
            ));
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] EmployeeReq request)
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
                        "Employee not found",
                        new[] { $"No employee with ID {id}" }
                    ));
                }

                return Ok(ApiResponse<EmployeeRes>.SuccessResponse(
                    updated,
                    "Employee updated successfully"
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
