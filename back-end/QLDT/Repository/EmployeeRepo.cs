using QLDT.Models;

namespace QLDT.Repository
{
    public interface EmployeeRepo
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee> CreateAsync(Employee e);
        Task<Employee?> GetByIdAsync(long id);
        Task<Employee> UpdateAsync(Employee e);
    }
}
