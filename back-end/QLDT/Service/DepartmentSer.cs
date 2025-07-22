// Service/DepartmentSer.cs
using QLDT.Dtos.request;
using QLDT.Dtos.response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLDT.Service
{
    public interface DepartmentSer
    {
        Task<IEnumerable<DepartmentRes>> GetAllAsync();
        Task<IEnumerable<DepartmentRes>> GetAllByUserAsync();
        Task<DepartmentRes?> GetByIdAsync(long id);
        Task<DepartmentRes> CreateAsync(DepartmentReq req);
        Task<DepartmentRes?> UpdateAsync(long id, DepartmentReq req);
    }
}
