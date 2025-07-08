using Microsoft.EntityFrameworkCore;
using QLDT.Data;
using QLDT.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLDT.Repository
{
    public interface UserRepo
    {
        Task<User> GetUserByUsernameAsync(string username);
        Task<User> GetUserByIdAsync(long Id);
        Task<User> CreateAsync(User user);
        Task<User> UpdateAsync(User user);
    }
}
