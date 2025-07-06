using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLDT.Service;

namespace QLDT.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Authorize]
	public class DashboardController : ControllerBase
	{
		private readonly IDashboardSer _ser;
		public DashboardController(IDashboardSer ser) => _ser = ser;

		[HttpGet]
		public async Task<IActionResult> Get()
			=> Ok(await _ser.GetSummaryAsync());
	}
}
