using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using QLDT.Models;

namespace QLDT.Repository
{
    public interface PartRepo
    {
        Task<IEnumerable<Part>> GetAllAsync();
        Task<IEnumerable<Part>> GetAllByUsernameAsync(string username);
        Task<Part> CreateAsync(Part e);
        Task<Part?> GetByIdAsync(long id);
        Task<Part> UpdateAsync(Part e);
    }
}