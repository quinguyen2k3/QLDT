using QLDT.Dtos.request;
using QLDT.Dtos.response;

namespace QLDT.Service
{
    public interface EmployeeSer
    {
        Task<IEnumerable<EmployeeRes>> GetAllAsync();
        Task<EmployeeRes> CreateAsync(EmployeeReq request);
        Task<EmployeeRes?> GetByIdAsync(long id);
        Task<EmployeeRes?> UpdateAsync(long id, EmployeeReq request);
    }
}
