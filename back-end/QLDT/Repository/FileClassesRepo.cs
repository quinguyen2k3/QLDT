using QLDT.Models;

namespace QLDT.Repository
{
    public interface FileClassesRepo
    {
        Task<IEnumerable<FileClass>> SaveAllAsync(IEnumerable<FileClass> fileClasses);
        Task DeleteByIdsAsync(List<string> ids);
        Task<IEnumerable<FileClass>> GetByClassIdAsync(long id);
    }
}
