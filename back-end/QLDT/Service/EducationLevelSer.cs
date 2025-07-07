using System.Collections.Generic;
using System.Threading.Tasks;
using QLDT.Dtos.request;
using QLDT.Dtos.response;
using QLDT.Models;

namespace QLDT.Service
{
    public interface EducationLevelSer
    {
        Task<IEnumerable<EducationLevelRes>> GetAllAsync();
        Task<EducationLevelRes?> GetByIdAsync(long id);
        Task<EducationLevelRes> CreateAsync(EducationLevelReq req);
        Task<bool> UpdateAsync(long id, EducationLevelReq req);
        Task<bool> DeleteAsync(long id);
    }
}
