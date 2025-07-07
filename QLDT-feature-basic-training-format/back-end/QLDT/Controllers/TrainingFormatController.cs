using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLDT.Dtos.request;
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TrainingFormatReq request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _service.CreateAsync(request);

            return Ok(created);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var data = await _service.GetByIdAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] TrainingFormatReq request)
        {
            var updated = await _service.UpdateAsync(id, request);
            if (updated == null)
            {
                return NotFound();
            }
            return Ok(updated);
        }

    }
}