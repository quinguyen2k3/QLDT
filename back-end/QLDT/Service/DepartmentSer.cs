using System.Collections.Generic;
using System.Threading.Tasks;
using QLDT.Dtos.request;
using QLDT.Dtos.response;

namespace QLDT.Service
{
    public interface DepartmentSer
    {
        Task<IEnumerable<DepartmentRes>> GetAllAsync();

        Task<DepartmentRes?> GetByIdAsync(long id);

        Task<DepartmentRes> CreateAsync(DepartmentReq req);

        Task<bool> UpdateAsync(long id, DepartmentReq req);
    }
}