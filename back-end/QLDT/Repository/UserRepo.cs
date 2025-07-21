using Microsoft.EntityFrameworkCore;
using QLDT.Data;
using QLDT.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLDT.Repository
{
    public interface UserRepo
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByIdAsync(long Id);
        Task<User> CreateAsync(User user);
        Task<User> UpdateAsync(User user);
    }
}
