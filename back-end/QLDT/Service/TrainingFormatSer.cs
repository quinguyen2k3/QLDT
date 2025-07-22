using QLDT.Dtos.request;
using QLDT.Dtos.response;
using QLDT.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLDT.Service
{
    public interface TrainingFormatSer
    {
        Task<IEnumerable<TrainingFormatRes>> GetAllAsync();
        Task<IEnumerable<TrainingFormatRes>> GetAllActiveAsync();
        Task<TrainingFormatRes> CreateAsync(TrainingFormatReq request);
        Task<TrainingFormatRes?> GetByIdAsync(long id);
        Task<TrainingFormatRes?> UpdateAsync(long id, TrainingFormatReq request);
        Task<IEnumerable<TrainingFormatRes>> GetFormat1And2Async();
       
    }
}