using QLDT.Dtos.request;
using QLDT.Dtos.response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLDT.Service
{
    public interface TrainingUnitSer
    {
        Task<IEnumerable<TrainingUnitRes>> GetAllAsync();
        Task<IEnumerable<TrainingUnitRes>> GetAllActiveAsync();
        Task<TrainingUnitRes> CreateAsync(TrainingUnitReq req);
        Task<TrainingUnitRes?> GetByIdAsync(long id);
        Task<TrainingUnitRes?> UpdateAsync(long id, TrainingUnitReq req);
    }
}