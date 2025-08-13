using AutoMapper;
using QLDT.Dtos.request;
using QLDT.Dtos.response;
using QLDT.Repository;

namespace QLDT.Service
{
    public interface CreditHourseSer
    {
        Task<IEnumerable<CreditHourseRes>> GetAllAsync();
        Task<IEnumerable<CreditHourseRes>> GetAllActiveAsync();
        Task<CreditHourseRes> CreateAsync(CreditHourseReq req);
        Task<CreditHourseRes?> GetByIdAsync(long id);
        Task<CreditHourseRes?> UpdateAsync(long id, CreditHourseReq req);
    }
}
