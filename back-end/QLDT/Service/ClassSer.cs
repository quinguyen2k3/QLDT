using QLDT.Dtos.request;
using QLDT.Dtos.response;

namespace QLDT.Service
{
    public interface ClassSer
    {
        Task<IEnumerable<ClassRes>> GetAllAsync();
        Task<IEnumerable<ClassRes>> GetAllByFormatAsync(long id);
        Task<IEnumerable<ClassRes>> GetAllByUsernameAsync();
        Task<ClassRes> CreateAsync(ClassReq request);
        Task<ClassRes?> GetByIdAsync(long id);
        Task<ClassRes> UpdateAsync(long id, ClassReq request);
    }
}
