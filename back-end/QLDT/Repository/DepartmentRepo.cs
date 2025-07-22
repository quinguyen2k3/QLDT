using System.Collections.Generic;
using System.Threading.Tasks;
using QLDT.Models;

namespace QLDT.Repository
{
    public interface DepartmentRepo
    {
        Task<IEnumerable<Department>> GetAllAsync();
        Task<IEnumerable<Department>> GetAllByUserAsync(string username);
        Task<Department> CreateAsync(Department e);
        Task<Department?> GetByIdAsync(long id);
        Task<Department> UpdateAsync(Department e);
    }
}