using System.Collections.Generic;
using System.Threading.Tasks;
using QLDT.Models;

namespace QLDT.Repository
{
    public interface PartRepo
    {
        Task<IEnumerable<Part>> GetAllAsync();
        Task<Part> CreateAsync(Part e);
        Task<Part?> GetByIdAsync(long id);
        Task<Part> UpdateAsync(Part e);
    }
}