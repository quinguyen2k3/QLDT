using System.Collections.Generic;
using System.Threading.Tasks;
using QLDT.Dtos.request;
using QLDT.Dtos.response;
using QLDT.Models;
namespace QLDT.Service
{
    public interface TrainingUnitSer
    {
        Task<IEnumerable<TrainingUnitRes>> GetAllAsync();
        Task<TrainingUnitRes?> GetByIdAsync(long id);
        Task<TrainingUnitRes> CreateAsync(TrainingUnitReq req);
        Task<bool> UpdateAsync(long id, TrainingUnitReq req);
        Task<bool> DeleteAsync(long id);
    }
}
