using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLDT.Dtos.response;
using QLDT.Service;

namespace QLDT.Controllers
{
    [Route("api/role")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleSer _service;

        public RoleController(RoleSer service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<RoleRes>>.SuccessResponse(
                data,
                "Fetched roles successfully"
            ));
        }
    }
}
