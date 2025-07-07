using System.Collections.Generic;
using System.Threading.Tasks;
using QLDT.Dtos.Request;
using QLDT.Dtos.Response;

namespace QLDT.Service
{
    public interface ITrainingFormatSer
    {
        Task<IEnumerable<TrainingFormatRes>> GetAllAsync();
        Task<TrainingFormatRes?> GetByIdAsync(long id);
        Task<TrainingFormatRes> CreateAsync(TrainingFormatReq req);
        Task<bool> UpdateAsync(long id, TrainingFormatReq req);
        Task<bool> DeleteAsync(long id);
    }
}