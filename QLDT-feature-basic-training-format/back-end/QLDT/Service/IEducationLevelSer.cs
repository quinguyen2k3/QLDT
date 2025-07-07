using System.Collections.Generic;
using System.Threading.Tasks;
using QLDT.Dtos.request;
using QLDT.Dtos.response;

namespace QLDT.Service
{
    public interface IEducationLevelSer
    {
        Task<IEnumerable<EducationLevelRes>> GetAllAsync();
        Task<EducationLevelRes?> GetByIdAsync(long id);
        Task<EducationLevelRes> CreateAsync(EducationLevelReq req);
        Task<bool> UpdateAsync(long id, EducationLevelReq req);
        Task<bool> DeleteAsync(long id);
    }
}
