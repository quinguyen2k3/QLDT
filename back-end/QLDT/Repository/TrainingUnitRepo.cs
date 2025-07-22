using System.Collections.Generic;
using System.Threading.Tasks;
using QLDT.Models;

namespace QLDT.Repository
{
    public interface TrainingUnitRepo
    {
        Task<IEnumerable<TrainingUnit>> GetAllAsync();

        Task<IEnumerable<TrainingUnit>> GetAllIsActiveAsync();

        Task<TrainingUnit> CreateAsync(TrainingUnit e);

        Task<TrainingUnit?> GetByIdAsync(long id);

        Task<TrainingUnit> UpdateAsync(TrainingUnit e);
    }
}