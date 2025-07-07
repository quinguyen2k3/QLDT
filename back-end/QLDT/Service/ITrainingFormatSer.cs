using System.Collections.Generic;
using System.Threading.Tasks;
using QLDT.Dtos.request;
using QLDT.Dtos.response;
using QLDT.Models;
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