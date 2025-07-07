using System.Collections.Generic;
using System.Threading.Tasks;
using QLDT.Models;

namespace QLDT.Repository
{
    public interface IPartRepo
    {
        Task<IEnumerable<Part>> GetAllAsync();
        Task<Part?> GetByIdAsync(long id);
        Task<Part> CreateAsync(Part e);
        Task<Part> UpdateAsync(Part e);
        Task DeleteAsync(Part e);
        Task SaveChangesAsync();
    }
}