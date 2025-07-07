using System.Collections.Generic;
using System.Threading.Tasks;
using QLDT.Dtos.request;
using QLDT.Dtos.response;

namespace QLDT.Service
{
    public interface ITrainingUnitSer
    {
        Task<IEnumerable<TrainingUnitRes>> GetAllAsync();
        Task<TrainingUnitRes?> GetByIdAsync(long id);
        Task<TrainingUnitRes> CreateAsync(TrainingUnitReq req);
        Task<bool> UpdateAsync(long id, TrainingUnitReq req);
        Task<bool> DeleteAsync(long id);
    }
}
