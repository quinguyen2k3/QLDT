using QLDT.Dtos.request;
using QLDT.Dtos.response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLDT.Service
{
    public interface TrainingFormatSer
    {
        Task<IEnumerable<TrainingFormatRes>> GetAllAsync();
        Task<TrainingFormatRes> CreateAsync(TrainingFormatReq request);
        Task<TrainingFormatRes?> GetByIdAsync(long id);
        Task<TrainingFormatRes?> UpdateAsync(long id, TrainingFormatReq request);
    }
}