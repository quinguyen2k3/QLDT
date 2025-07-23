using QLDT.Dtos.request;
using QLDT.Dtos.response;

namespace QLDT.Service
{
    public interface MajorSer
    {
        Task<IEnumerable<MajorRes>> GetAllAsync();
        Task<IEnumerable<MajorRes>> GetAllActiveAsync();
        Task<IEnumerable<MajorRes>> GetAllByUserAsync();
        Task<MajorRes> CreateAsync(MajorReq req);
        Task<MajorRes?> GetByIdAsync(long id);
        Task<MajorRes?> UpdateAsync(long id, MajorReq req);
    }
}
