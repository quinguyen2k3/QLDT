using System.Collections.Generic;
using System.Threading.Tasks;
using QLDT.Models;

namespace QLDT.Repository
{
    public interface EducationLevelRepo
    {
        Task<IEnumerable<EducationLevel>> GetAllAsync();
        Task<EducationLevel?> GetByIdAsync(long id);
        Task<EducationLevel> CreateAsync(EducationLevel e);
        Task<EducationLevel> UpdateAsync(EducationLevel e);
        Task DeleteAsync(EducationLevel e);
        Task SaveChangesAsync();
    }
}