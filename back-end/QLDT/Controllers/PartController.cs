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
    [Route("api/parts")]
    [ApiController]
    public class PartController : ControllerBase
    {
        private readonly PartSer _service;
        public PartController(PartSer service) => _service = service;

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<PartRes>>.SuccessResponse(
                data, "Fetched parts successfully"
            ));
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null)
                return NotFound(ApiResponse<string>.ErrorResponse(
                    "Part not found",
                    new[] { $"No part with ID {id}." }
                ));

            return Ok(ApiResponse<PartRes>.SuccessResponse(
                dto, "Fetched part successfully"
            ));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PartReq req)
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
                ApiResponse<PartRes>.SuccessResponse(
                    created, "Part created successfully"
                ));
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] PartReq req)
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
                    "Part not found",
                    new[] { $"No part with ID {id}." }
                ));

            return Ok(ApiResponse<PartRes>.SuccessResponse(
                updated, "Part updated successfully"
            ));
        }
    }
}
