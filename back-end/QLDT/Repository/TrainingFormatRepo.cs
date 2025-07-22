using QLDT.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLDT.Repository
{
    public interface TrainingFormatRepo
    {
        Task<IEnumerable<TrainingFormat>> GetAllAsync();
        Task<IEnumerable<TrainingFormat>> GetAllIsActiveAsync();
        Task<TrainingFormat> CreateAsync(TrainingFormat entity);
        Task<TrainingFormat?> GetByIdAsync(long id);
        Task<TrainingFormat> UpdateAsync(TrainingFormat entity);
        Task<IEnumerable<TrainingFormat>> GetById1And2Async();
    }
}
