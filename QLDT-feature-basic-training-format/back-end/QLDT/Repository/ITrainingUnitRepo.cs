using System.Collections.Generic;
using System.Threading.Tasks;
using QLDT.Models;

namespace QLDT.Repository
{
    public interface ITrainingUnitRepo
    {
        Task<IEnumerable<TrainingUnit>> GetAllAsync();
        Task<TrainingUnit?> GetByIdAsync(long id);
        Task<TrainingUnit> CreateAsync(TrainingUnit entity);
        Task<TrainingUnit> UpdateAsync(TrainingUnit entity);
        Task DeleteAsync(TrainingUnit entity);
        Task SaveChangesAsync();
    }
}