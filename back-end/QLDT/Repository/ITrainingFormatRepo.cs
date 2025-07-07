using System.Collections.Generic;
using System.Threading.Tasks;
using QLDT.Models;

namespace QLDT.Repository
{
    public interface ITrainingFormatRepo
    {
        Task<IEnumerable<TrainingFormat>> GetAllAsync();
        Task<TrainingFormat?> GetByIdAsync(long id);
        Task<TrainingFormat> CreateAsync(TrainingFormat e);
        Task<TrainingFormat> UpdateAsync(TrainingFormat e);
        Task DeleteAsync(TrainingFormat e);
        Task SaveChangesAsync();
    }
}