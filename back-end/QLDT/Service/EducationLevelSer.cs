using QLDT.Dtos.request;
using QLDT.Dtos.response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLDT.Service
{
    public interface EducationLevelSer
    {
        Task<IEnumerable<EducationLevelRes>> GetAllAsync();
        Task<IEnumerable<EducationLevelRes>> GetAllActiveAsync();
        Task<EducationLevelRes> CreateAsync(EducationLevelReq req);
        Task<EducationLevelRes?> GetByIdAsync(long id);
        Task<EducationLevelRes?> UpdateAsync(long id, EducationLevelReq req);
    }
}