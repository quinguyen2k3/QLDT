using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}
