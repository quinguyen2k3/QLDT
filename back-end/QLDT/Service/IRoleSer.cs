using System.Collections.Generic;
using System.Threading.Tasks;
using QLDT.Dtos.request;
using QLDT.Dtos.response;

using QLDT.Models;

namespace QLDT.Service
{
	public interface IRoleSer
	{
		Task<IEnumerable<RoleRes>> GetAllAsync();
		Task<RoleRes?> GetByIdAsync(long id);
		Task<RoleRes> CreateAsync(RoleReq req);
		Task<bool> UpdateAsync(long id, RoleReq req);
		Task<bool> DeleteAsync(long id);
	}
}