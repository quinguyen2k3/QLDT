using System.Collections.Generic;
using System.Threading.Tasks;
using QLDT.Models;

namespace QLDT.Repository
{
    public interface EducationLevelRepo
    {
        Task<IEnumerable<EducationLevel>> GetAllAsync();

        Task<IEnumerable<EducationLevel>> GetAllIsActiveAsync();

        Task<EducationLevel> CreateAsync(EducationLevel e);

        Task<EducationLevel?> GetByIdAsync(long id);

        Task<EducationLevel> UpdateAsync(EducationLevel e);
    }
}