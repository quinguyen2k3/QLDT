using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLDT.Attribute;
using QLDT.Dtos.request;
using QLDT.Dtos.response;
using QLDT.Service;

namespace QLDT.Controllers
{
    [Route("api/class")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly ClassSer _service;

        public ClassController(ClassSer service)
        {
            _service = service;
        }

        [Authorize]
        [HasPermission("Report.ViewSummaryList")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<ClassRes>>.SuccessResponse(
                data,
                "Fetched class successfully"
            ));
        }

        [Authorize]
        [HasPermission("Report.ViewSummaryList")]
        [HttpGet("me")]
        public async Task<IActionResult> GetAllByUser()
        {
            var data = await _service.GetAllByUsernameAsync();
            return Ok(ApiResponse<IEnumerable<ClassRes>>.SuccessResponse(
                data,
                "Fetched class successfully"
            ));
        }

        [Authorize]
        [HasPermission("Class.Manage")]
        [HttpGet("format/{id}")]
        public async Task<IActionResult> GetAllByFormat(long id)
        {
            var data = await _service.GetAllByFormatAsync(id);
            return Ok(ApiResponse<IEnumerable<ClassRes>>.SuccessResponse(
                data,
                "Fetched class successfully"
            ));
        }

        [Authorize]
        [HasPermission("Class.Manage")]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ClassReq request)
        {

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(ApiResponse<string>.ErrorResponse(
                    "Validation failed",
                    errors
                ));
            }

            try
            {
                var classRes = await _service.CreateAsync(request);

                return Ok(ApiResponse<ClassRes>.SuccessResponse(
                    classRes,
                    "Class created successfully"
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
        [HasPermission("Class.Manage")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var data = await _service.GetByIdAsync(id);
            if (data == null)
            {
                return NotFound(ApiResponse<string>.ErrorResponse(
                    "Class not found",
                    new[] { $"No course with ID {id}" }
                ));
            }

            return Ok(ApiResponse<ClassRes>.SuccessResponse(
                data,
                "Fetched Class successfully"
            ));
        }

        [Authorize]
        [HasPermission("Class.Manage")]
        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update(long id, [FromForm] ClassReq request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(ApiResponse<string>.ErrorResponse(
                    "Validation failed",
                    errors
                ));
            }
            try
            {
                var updatedClass = await _service.UpdateAsync(id, request);
                return Ok(new
                {
                    success = true,
                    data = updatedClass,
                    message = "Class updated successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }
    }
}
