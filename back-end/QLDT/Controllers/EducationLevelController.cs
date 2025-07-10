using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLDT.Dtos.request;
using QLDT.Dtos.response;
using QLDT.Service;

namespace QLDT.Controllers
{
    [Route("api/levels")]
    [ApiController]
    public class EducationLevelController : ControllerBase
    {
        private readonly EducationLevelSer _service;
        public EducationLevelController(EducationLevelSer service) => _service = service;

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<EducationLevelRes>>.SuccessResponse(
                data, "Fetched education levels successfully"
            ));
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null)
                return NotFound(ApiResponse<string>.ErrorResponse(
                    "Education level not found",
                    new[] { $"No education level with ID {id}." }
                ));

            return Ok(ApiResponse<EducationLevelRes>.SuccessResponse(
                dto, "Fetched education level successfully"
            ));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EducationLevelReq req)
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

            var created = await _service.CreateAsync(req);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<EducationLevelRes>.SuccessResponse(
                    created, "Education level created successfully"
                ));
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] EducationLevelReq req)
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

            var updated = await _service.UpdateAsync(id, req);
            if (updated == null)
                return NotFound(ApiResponse<string>.ErrorResponse(
                    "Education level not found",
                    new[] { $"No education level with ID {id}." }
                ));

            return Ok(ApiResponse<EducationLevelRes>.SuccessResponse(
                updated, "Education level updated successfully"
            ));
        }
    }
}
