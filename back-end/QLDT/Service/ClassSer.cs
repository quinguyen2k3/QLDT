using QLDT.Dtos.request;
using QLDT.Dtos.response;

namespace QLDT.Service
{
    public interface ClassSer
    {
        Task<IEnumerable<ClassRes>> GetAllAsync(long id);
        Task<IEnumerable<ClassRes>> GetAllByUserAsync(long id);
        Task<ClassRes> CreateAsync(ClassReq request);
        Task<ClassRes?> GetByIdAsync(long id);
        Task<ClassRes> UpdateAsync(long id, ClassReq request);
    }
}
