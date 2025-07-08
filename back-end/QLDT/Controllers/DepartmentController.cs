using Microsoft.AspNetCore.Mvc;
using QLDT.Dtos.request;
using QLDT.Service;

namespace QLDT.Controllers
{
    [ApiController]
    [Route("api/department")]
    public class DepartmentController : ControllerBase
    {
        private readonly DepartmentSer _service;
        public DepartmentController(DepartmentSer service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var dto = await _service.GetByIdAsync(id);
            return dto == null ? NotFound() : Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DepartmentReq req)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var dto = await _service.CreateAsync(req);
            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] DepartmentReq req)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return await _service.UpdateAsync(id, req)
                ? NoContent()
                : NotFound();
        }
    }
}