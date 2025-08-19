using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLDT.Dtos.response;
using QLDT.Service;

namespace QLDT.Controllers
{
    [Route("api/permission")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly PermissionSer _service;

        public PermissionController(PermissionSer service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllByUserAsync();
            return Ok(ApiResponse<IEnumerable<PermissionRes>>.SuccessResponse(
                data,
                "Fetched permissions successfully"
            ));
        }
    }
}
