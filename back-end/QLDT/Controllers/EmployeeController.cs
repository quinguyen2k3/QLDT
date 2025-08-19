using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLDT.Attribute;
using QLDT.Dtos.request;
using QLDT.Dtos.response;
using QLDT.Service;

namespace QLDT.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeSer _service;

        public EmployeeController(EmployeeSer service)
        {
            _service = service;
        }

        [Authorize]
        [HasPermission("Report.ViewSummaryList")]
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
        [HasPermission("Report.ViewOwnList")]
        [HttpGet("me")]
        public async Task<IActionResult> GetAllByMe()
        {
            var data = await _service.GetAllByUserAsync();
            return Ok(ApiResponse<IEnumerable<EmployeeRes>>.SuccessResponse(
                data,
                "Fetched employees successfully"
            ));
        }

        [Authorize]
        [HasPermission("Report.ViewOwnList")]
        [HttpGet("department/me")]
        public async Task<IActionResult> GetAllByDepartmentMe()
        {
            var data = await _service.GetAllByCurrentUserDepartmentAsync();
            return Ok(ApiResponse<IEnumerable<EmployeeRes>>.SuccessResponse(
                data,
                "Fetched employees successfully"
            ));
        }

        [Authorize]
        [HasPermission("Employee.Manage")]
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
        [HasPermission("Report.ViewDetail")]
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
        [HasPermission("Report.ViewProcess")]
        [HttpGet("my-info")]
        public async Task<IActionResult> GetMyEmployeeInfo()
        {
            var data = await _service.GetByIdAsync();
            if (data == null)
            {
                return NotFound(ApiResponse<string>.ErrorResponse(
                    "Employee not found",
                    new[] { $"No employee fected" }
                ));
            }

            return Ok(ApiResponse<EmployeeRes>.SuccessResponse(
                data,
                "Fetched employee successfully"
            ));
        }

        [Authorize]
        [HasPermission("Employee.Manage")]
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
