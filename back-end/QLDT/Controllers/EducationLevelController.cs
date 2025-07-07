using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QLDT.Dtos.request;
using QLDT.Dtos.response;
using QLDT.Service;

namespace QLDT.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EducationLevelController : ControllerBase
    {
        private readonly IEducationLevelSer _ser;
        public EducationLevelController(IEducationLevelSer ser) => _ser = ser;

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _ser.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var dto = await _ser.GetByIdAsync(id);
            return dto == null ? NotFound() : Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EducationLevelReq req)
        {
            var dto = await _ser.CreateAsync(req);
            return CreatedAtAction(nameof(Get), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] EducationLevelReq req)
        {
            if (!await _ser.UpdateAsync(id, req)) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            if (!await _ser.DeleteAsync(id)) return NotFound();
            return NoContent();
        }
    }
