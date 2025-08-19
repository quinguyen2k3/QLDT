using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLDT.Attribute;
using QLDT.Dtos.request;
using QLDT.Dtos.response;
using QLDT.Service;


namespace QLDT.Controllers
{
    [Route("api/course")]
    [ApiController]
    public class CourseController : ControllerBase
    {

        private readonly CourseSer _service;

        public CourseController(CourseSer service)
        {
            _service = service;
        }

        [Authorize]
        [HasPermission("Report.ViewSummaryList")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<CourseRes>>.SuccessResponse(
                data,
                "Fetched departments successfully"
            ));
        }

        [Authorize]
        [HasPermission("Report.ViewOwnList")]
        [HttpGet("me")]
        public async Task<IActionResult> GetAllByMe()
        {
            var data = await _service.GetAllByUserAsync();
            return Ok(ApiResponse<IEnumerable<CourseRes>>.SuccessResponse(
                data,
                "Fetched departments successfully"
            ));
        }

        [Authorize]
        [HasPermission("Report.ViewActiveList")]
        [HttpGet("active")]
        public async Task<IActionResult> GetAllActive()
        {
            var data = await _service.GetAllActiveAsync();
            return Ok(ApiResponse<IEnumerable<CourseRes>>.SuccessResponse(
                data,
                "Fetched departments successfully"
            ));
        }

        [Authorize]
        [HasPermission("Course.Manage")]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CourseReq request)
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
                var courseRes = await _service.CreateAsync(request);

                return Ok(ApiResponse<CourseRes>.SuccessResponse(
                    courseRes,
                    "Course created successfully"
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
        [HasPermission("Course.Manage")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var data = await _service.GetByIdAsync(id);
            if (data == null)
            {
                return NotFound(ApiResponse<string>.ErrorResponse(
                    "Course not found",
                    new[] { $"No course with ID {id}" }
                ));
            }

            return Ok(ApiResponse<CourseRes>.SuccessResponse(
                data,
                "Fetched Course successfully"
            ));
        }

        [Authorize]
        [HasPermission("Course.Manage")]
        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update(long id, [FromForm] CourseReq request)
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
                var updatedCourse = await _service.UpdateAsync(id, request);
                return Ok(new
                {
                    success = true,
                    data = updatedCourse,
                    message = "Course updated successfully"
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
