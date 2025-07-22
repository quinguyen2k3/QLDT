using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLDT.Dtos.request;
using QLDT.Dtos.response;
using QLDT.Service;

namespace QLDT.Controllers
{
    [Route("api/department")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly DepartmentSer _service;

        public DepartmentController(DepartmentSer service)
        {
            _service = service;
        }

        // GET /api/department
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<DepartmentRes>>.SuccessResponse(
                data,
                "Fetched departments successfully"
            ));
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetAllByMe()
        {
            var data = await _service.GetAllByUserAsync();
            return Ok(ApiResponse<IEnumerable<DepartmentRes>>.SuccessResponse(
                data,
                "Fetched departments successfully"
            ));
        }

        // POST /api/department
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DepartmentReq request)
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
                    ApiResponse<DepartmentRes>.SuccessResponse(
                        created,
                        "Department created successfully"
                    ));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.ErrorResponse(
                    "System error", new[] { ex.Message }
                ));
            }
        }

        // GET /api/department/{id}
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var data = await _service.GetByIdAsync(id);
            if (data == null)
            {
                return NotFound(ApiResponse<string>.ErrorResponse(
                    "Department not found",
                    new[] { $"No department with ID {id}" }
                ));
            }

            return Ok(ApiResponse<DepartmentRes>.SuccessResponse(
                data,
                "Fetched department successfully"
            ));
        }

        // PUT /api/department/{id}
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] DepartmentReq request)
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
                        "Department not found",
                        new[] { $"No department with ID {id}" }
                    ));
                }

                return Ok(ApiResponse<DepartmentRes>.SuccessResponse(
                    updated,
                    "Department updated successfully"
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
