using Microsoft.EntityFrameworkCore;
using QLDT.Data;
using QLDT.Dtos.Response;

namespace QLDT.Service.impl
{
	public class DashboardSerImpl : IDashboardSer
	{
		private readonly ApplicationDbContext _ctx;
		public DashboardSerImpl(ApplicationDbContext ctx) => _ctx = ctx;

		public async Task<DashboardRes> GetSummaryAsync()
		{
			return new DashboardRes
			{
				TotalUsers = await _ctx.Users.CountAsync(),
				TotalCourses = await _ctx.Courses.CountAsync(),
				TotalDepartments = await _ctx.Departments.CountAsync()
			};
		}
	}
}
