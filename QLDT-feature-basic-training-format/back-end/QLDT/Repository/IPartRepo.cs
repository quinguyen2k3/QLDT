using System.Collections.Generic;
using System.Threading.Tasks;
using QLDT.Models;

namespace QLDT.Repository
{
    public interface IPartRepo
    {
        Task<IEnumerable<Part>> GetAllAsync();
        Task<Part?> GetByIdAsync(long id);
        Task<Part> CreateAsync(Part entity);
        Task<Part> UpdateAsync(Part entity);
        Task DeleteAsync(Part entity);
        Task SaveChangesAsync();
    }
}
